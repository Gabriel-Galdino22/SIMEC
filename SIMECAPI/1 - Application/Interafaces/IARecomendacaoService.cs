using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using SIMECAPI.Models;

namespace SIMECAPI.Services
{
    public class IARecomendacaoService : IIARecomendacaoService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _modelo;
        private const string ModeloPath = "modelo_consumo.zip"; // Caminho para salvar o modelo

        public IARecomendacaoService()
        {
            _mlContext = new MLContext();
            CarregarModelo(); // Carrega o modelo salvo ao iniciar
        }

        public void TreinarModelo(List<ConsumoEnergia> consumos)
        {
            if (consumos == null || consumos.Count == 0)
                throw new Exception("Nenhum dado de consumo foi encontrado para treinamento.");

            // Logs para verificar os dados
            Console.WriteLine("Dados de consumo recebidos para treinamento:");
            foreach (var consumo in consumos)
            {
                Console.WriteLine($"DataLeitura: {consumo.DataLeitura}, ConsumoKwh: {consumo.ConsumoKwh}");
            }

            var dataView = _mlContext.Data.LoadFromEnumerable(consumos.Select(c => new ConsumoEnergiaData
            {
                ConsumoKwh = (float)c.ConsumoKwh,
                DataLeitura = (float)((double)c.DataLeitura.Ticks / 1e7), // Normalizar ticks
                Label = (float)c.ConsumoKwh
            }));

            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(ConsumoEnergiaData.ConsumoKwh), nameof(ConsumoEnergiaData.DataLeitura))
                .Append(_mlContext.Regression.Trainers.Sdca());

            _modelo = pipeline.Fit(dataView);

            // Salvar o modelo treinado
            _mlContext.Model.Save(_modelo, dataView.Schema, ModeloPath);
            Console.WriteLine("Modelo treinado e salvo com sucesso!");
        }

        public string GerarRecomendacao(ConsumoEnergia consumoAtual)
        {
            if (_modelo == null)
                return "Modelo não está treinado.";

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ConsumoEnergiaData, ConsumoPrediction>(_modelo);

            var prediction = predictionEngine.Predict(new ConsumoEnergiaData
            {
                ConsumoKwh = (float)consumoAtual.ConsumoKwh,
                DataLeitura = (float)((double)consumoAtual.DataLeitura.Ticks / 1e7)
            });

            return $"Com base no consumo atual ({consumoAtual.ConsumoKwh} kWh), reduza o uso em {prediction.Score:0.00}%";
        }

        private void CarregarModelo()
        {
            if (System.IO.File.Exists(ModeloPath))
            {
                _modelo = _mlContext.Model.Load(ModeloPath, out _);
                Console.WriteLine("Modelo carregado com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhum modelo salvo encontrado.");
            }
        }
    }
}

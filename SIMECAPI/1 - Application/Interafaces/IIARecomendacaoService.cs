using SIMECAPI.Models;

namespace SIMECAPI.Services
{
    public interface IIARecomendacaoService
    {
        void TreinarModelo(List<ConsumoEnergia> consumos);
        string GerarRecomendacao(ConsumoEnergia consumoAtual);
    }
}

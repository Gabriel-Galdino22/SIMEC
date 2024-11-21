namespace SIMECAPI.Services
{
    public class ConsumoEnergiaData
    {
        public float ConsumoKwh { get; set; } // Usar float para compatibilidade com ML.NET
        public float DataLeitura { get; set; } // Data armazenada como float normalizado
        public float Label { get; set; } // Usado como rótulo (target) no ML.NET
    }
}

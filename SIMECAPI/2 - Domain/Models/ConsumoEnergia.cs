using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMECAPI.Models
{
    [Table("CONSUMOENERGIA")]
    public class ConsumoEnergia
    {
        [Key]
        [Column("ID_CONSUMO")]
        public int IdConsumo { get; set; }

        [Column("DATA_LEITURA")]
        public DateTime DataLeitura { get; set; }

        [Column("CONSUMO_KWH")]
        public decimal ConsumoKwh { get; set; }

        [Column("ID_APARTAMENTO")]
        public int IdApartamento { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ID_DICA")]
        public int IdDica { get; set; }
    }
}

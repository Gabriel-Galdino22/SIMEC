using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMECAPI.Models
{
    [Table("MOEDA")]
    public class Moeda
    {
        [Key]
        [Column("ID_MOEDA")]
        public int IdMoeda { get; set; }

        [Column("QUANTIDADE")]
        public int Quantidade { get; set; }

        [Column("DATA_ULT_ATLZ")]
        public DateTime DataUltimaAtualizacao { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ID_DICA")]
        public int IdDica { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMECAPI.Models
{
    [Table("DICASUSTENTABILIDADE")]
    public class DicaSustentabilidade
    {
        [Key]
        [Column("ID_DICA")]
        public int IdDica { get; set; }

        [Column("TITULO")]
        public string Titulo { get; set; } = string.Empty;

        [Column("DESCRICAO")]
        public string Descricao { get; set; } = string.Empty;

        [Column("DATA_CRIACAO")]
        public DateTime DataCriacao { get; set; }
    }
}

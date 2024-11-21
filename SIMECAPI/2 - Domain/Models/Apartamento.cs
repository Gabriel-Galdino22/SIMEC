using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMECAPI.Models
{
    [Table("APARTAMENTO")]
    public class Apartamento
    {
        [Key]
        [Column("ID_APARTAMENTO")]
        public int IdApartamento { get; set; }

        [Column("NUMERO")]
        public string Numero { get; set; } = string.Empty;

        [Column("ANDAR")]
        public int Andar { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ID_DICA")]
        public int IdDica { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMECAPI.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("NOME")]
        public string Nome { get; set; } = string.Empty;

        [Column("EMAIL")]
        public string Email { get; set; } = string.Empty;

        [Column("SENHA")]
        public string Senha { get; set; } = string.Empty;

        [Column("DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("TIPO_USUARIO")]
        public string TipoUsuario { get; set; } = string.Empty;

        [Column("ID_DICA")]
        public int IdDica { get; set; }
    }
}

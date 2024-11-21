using Microsoft.EntityFrameworkCore;
using SIMECAPI.Models;

namespace SIMECAPI.Data
{
    public class SIMECContext : DbContext
    {
        public SIMECContext(DbContextOptions<SIMECContext> options) : base(options) { }

        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<ConsumoEnergia> ConsumoEnergias { get; set; }
        public DbSet<DicaSustentabilidade> DicasSustentabilidades { get; set; }
        public DbSet<Moeda> Moedas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar entidades para corresponder aos nomes no banco de dados

            modelBuilder.Entity<Apartamento>(entity =>
            {
                entity.ToTable("APARTAMENTO");
                entity.Property(e => e.IdApartamento).HasColumnName("ID_APARTAMENTO");
                entity.Property(e => e.Numero).HasColumnName("NUMERO");
                entity.Property(e => e.Andar).HasColumnName("ANDAR");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.IdDica).HasColumnName("ID_DICA");
            });

            modelBuilder.Entity<ConsumoEnergia>(entity =>
            {
                entity.ToTable("CONSUMOENERGIA");
                entity.Property(e => e.IdConsumo).HasColumnName("ID_CONSUMO");
                entity.Property(e => e.DataLeitura).HasColumnName("DATA_LEITURA");
                entity.Property(e => e.ConsumoKwh).HasColumnName("CONSUMO_KWH");
                entity.Property(e => e.IdApartamento).HasColumnName("ID_APARTAMENTO");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.IdDica).HasColumnName("ID_DICA");
            });

            modelBuilder.Entity<DicaSustentabilidade>(entity =>
            {
                entity.ToTable("DICASUSTENTABILIDADE");
                entity.Property(e => e.IdDica).HasColumnName("ID_DICA");
                entity.Property(e => e.Titulo).HasColumnName("TITULO");
                entity.Property(e => e.Descricao).HasColumnName("DESCRICAO");
                entity.Property(e => e.DataCriacao).HasColumnName("DATA_CRIACAO");
            });

            modelBuilder.Entity<Moeda>(entity =>
            {
                entity.ToTable("MOEDA");
                entity.Property(e => e.IdMoeda).HasColumnName("ID_MOEDA");
                entity.Property(e => e.Quantidade).HasColumnName("QUANTIDADE");
                entity.Property(e => e.DataUltimaAtualizacao).HasColumnName("DATA_ULT_ATLZ");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.IdDica).HasColumnName("ID_DICA");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Nome).HasColumnName("NOME");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.Senha).HasColumnName("SENHA");
                entity.Property(e => e.DataCadastro).HasColumnName("DATA_CADASTRO");
                entity.Property(e => e.TipoUsuario).HasColumnName("TIPO_USUARIO");
                entity.Property(e => e.IdDica).HasColumnName("ID_DICA");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

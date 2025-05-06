using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using iTasks.Models;

namespace iTasks.Data
{
    /// Contexto de dados EF6 Code-First para a aplicação iTasks.
    public class iTasksContexto : DbContext
    {
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<TipoTarefa> TiposTarefa { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 1) Sem pluralização automática
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // 2) Índice único no Username de Utilizador
            modelBuilder.Entity<Utilizador>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // 3) Índice único por Programador + Ordem
            modelBuilder.Entity<Tarefa>()
                .HasIndex(t => new { t.ProgramadorId, t.Ordem })
                .IsUnique();

            // 4) Mapear a coluna "Estado" (nvarchar existente) para a propriedade string
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.EstadoString)
                .HasColumnName("Estado")
                .HasColumnType("nvarchar")
                .HasMaxLength(4000)
                .IsRequired()
                .HasColumnAnnotation("DefaultValue", "ToDo");

            // 5) Ignorar o enum Estado, que só existe em memória
            modelBuilder.Entity<Tarefa>()
                .Ignore(t => t.Estado);

            //TESTE CHATGPT
            modelBuilder.Entity<Tarefa>()
                .HasRequired(t => t.Criador)
                .WithMany()
                .HasForeignKey(t => t.CriadorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tarefa>()
                .HasRequired(t => t.Programador)
                .WithMany()
                .HasForeignKey(t => t.ProgramadorId)
                .WillCascadeOnDelete(false);



            base.OnModelCreating(modelBuilder);
        }
    }
}

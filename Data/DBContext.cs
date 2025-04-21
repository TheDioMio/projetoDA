using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Configuration;
using iTasks.Models;
using System.Runtime.Remoting.Contexts;

namespace iTasks.Data
{
    /// <summary>
    /// Contexto de dados EF6 Code‑First para a aplicação iTasks.
    /// </summary>
    internal class iTasksContexto : DbContext
    {
        public iTasksContexto()
            : base("name=iTasksContexto")
        {
            // Opcional: inicializador de BD
            // Database.SetInitializer(new CreateDatabaseIfNotExists<iTasksContexto>());
        }

        // DbSet para cada entidade do modelo
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<TipoTarefa> TiposTarefa { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove pluralização automática de tabelas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Índice único no Username de Utilizador
            modelBuilder.Entity<Utilizador>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Índice único que garante apenas uma mesma ordem por programador
            modelBuilder.Entity<Tarefa>()
                .HasIndex(t => new { t.ProgramadorId, t.Ordem })
                .IsUnique();

            // Valor-padrão do Estado = "ToDo"
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Estado)
                .HasColumnType("nvarchar")
                .HasDefaultValue("ToDo");

            base.OnModelCreating(modelBuilder);
        }
    }
}

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
    /// Contexto de dados EF6 Code‑First para a aplicação iTasks.
    internal class iTasksContexto : DbContext
    {
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
                .HasIndex(utilizador => utilizador.Username)
                .IsUnique();

            // Índice único que garante apenas uma mesma ordem por programador
            modelBuilder.Entity<Tarefa>()
                .HasIndex(tarefa => new { tarefa.ProgramadorId, tarefa.Ordem })
                .IsUnique();

            // Valor-padrão do Estado = "ToDo"
            modelBuilder.Entity<Tarefa>()
                .Property(tarefa => tarefa.Estado)
                .HasColumnType("nvarchar")
                .HasColumnAnnotation("DefaultValue", "ToDo");
            base.OnModelCreating(modelBuilder);
        }
    }
}

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using iTasks.Models;

namespace iTasks.Data
{
    /// Contexto de dados EF6 Code-First para a aplicação iTasks.
    public class iTasksContexto : DbContext
    {
        public iTasksContexto()
        {
        }

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<TipoTarefa> TiposTarefa { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

    }
}

using iTasks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class TipoTarefa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        //public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
    
}

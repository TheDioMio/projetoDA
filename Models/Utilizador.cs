using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Utilizador
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string Nome { get; set; }

        public Utilizador(string username, string password, string nome)
        {
            Username = username;
            Password = password;
            Nome = nome;
                     

        }

        public Utilizador()
        {
        }
    }



}

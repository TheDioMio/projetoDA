using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    //public enum TipoUtilizador
    //{
    //    Programador,
    //    Gestor
    //}

    //public enum NivelExperiencia
    //{
    //    Junior,
    //    Senior
    //}

    //public enum Departamento
    //{
    //    IT,
    //    Marketing,
    //    Administracao
    //}
    public class Utilizador
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string Nome { get; set; }

        public Utilizador(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public Utilizador()
        {
        }
    }



}

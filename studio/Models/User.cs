using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks
{
    internal class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public NivelExp NivelExperiencia { get; set; }
        public Departamento Departamento { get; set; }

        //Enumeração dos tipos possíveis de users
        public enum TipoUtilizador
        {
            Programador,
            Gestor
        }
        public TipoUtilizador tipoUtilizador { get; set; }

        //Enumeração níveis de EXP
        public enum NivelExperiencia 
        {
            Junior,
            Senior
        }
        public NivelExperiencia nivelExperiencia { get; set; }

        //Enumeração departamento a que pertence
        public enum Departamento
        {
            IT,
            Marketing,
            Administracao
        }
        public Departamento departamento { get; set; }

    }
}

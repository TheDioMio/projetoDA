using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Gestor : Utilizador
    {

        public Departamento departamento { get; set; }
        public bool gereUtilizadores { get; set; }

        public Gestor()
        {
        }
    }

}




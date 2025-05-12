using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Programador : Utilizador
    {
        public Gestor Gestor { get; set; }
        public NivelExperiencia nivelExperiencia { get; set; }

        public Programador()
        {
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    //Amazena dados globais da sessão do user logado.
    internal class Sessao
    {
        public static Utilizador UtilizadorLogado { get; set; }
    }
}

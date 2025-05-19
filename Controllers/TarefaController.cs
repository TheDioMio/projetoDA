using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    public class TarefasController
    {
        iTasksContexto Contexto = new iTasksContexto(); //Criação de variável global para aceder ao contexto.

        public void AvancarTarefa(Tarefa tarefa)
        {
           if(tarefa != null)
           {
                switch(tarefa.EstadoAtual)
                {
                    case EstadoAtual.ToDo:
                        tarefa.EstadoAtual = EstadoAtual.Doing;
                        break;

                    case EstadoAtual.Doing:
                        tarefa.EstadoAtual = EstadoAtual.Done;
                        break;

                    case EstadoAtual.Done:
                        MessageBox.Show(
                            "ERRO: Tarefa já está concluída",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        break;
                }
                Contexto.SaveChanges();
            }
        }
    }
}

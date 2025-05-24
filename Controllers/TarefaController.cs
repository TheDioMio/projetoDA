using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    public class TarefaController
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

                        //FALTA VALIDAÇÕES DOS REQUESITOS PARA PASSAR DE TODO -> DOING
                        break;

                    case EstadoAtual.Doing:
                        tarefa.EstadoAtual = EstadoAtual.Done;
                        //FALTA VALIDAÇÕES DOS REQUESITOS PARA PASSAR DE DOING -> DONE
                        break;

                    case EstadoAtual.Done:
                        MessageBox.Show(
                            "ERRO: Tarefa já está concluída",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "ERRO: Erro a validar estado atual da tarefa (Função AvancarTarefa - TarefaController)",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;
                }
                Contexto.Entry(tarefa).State = EntityState.Modified;
                Contexto.SaveChanges();
            }
        }

        public void RetrocederTarefa(Tarefa tarefa) //FALTA VALIDAÇÕES
        {
            if (tarefa != null)
            {
                if (tarefa.EstadoAtual == EstadoAtual.Doing)
                {
                    tarefa.EstadoAtual = EstadoAtual.ToDo;

                    Contexto.Entry(tarefa).State = EntityState.Modified;
                    Contexto.SaveChanges();
                }
                else
                {
                    MessageBox.Show(
                        "ERRO: Só é possível recuar tarefas que estão em curso.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        //Adicionar tarefa
        public void Adicionar(Tarefa tarefa)
        {
            Contexto.Tarefas.Add(tarefa);
            Contexto.SaveChanges();
        }
    }
}

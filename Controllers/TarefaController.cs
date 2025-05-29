using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace iTasks.Controllers
{
    public class TarefaController
    {
        /*
         adicionar       feito
        alterar
        apagar

        procurar todas
        procurar por ordem
        procurar por id           feito
        procurar por programador
        procurar por gestor

         
         
         */

        iTasksContexto contexto = new iTasksContexto(); //Criação de variável global para aceder ao contexto.
        iTasksContexto Contexto = new iTasksContexto(); //Criação de variável global para aceder ao contexto.

        public bool Criar(Tarefa tarefa)
        {
            bool flag = false;
            try
            {
                contexto.Tarefas.Add(tarefa);
                contexto.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public void Atualizar(Tarefa tarefa)
        {
            contexto.Entry(tarefa).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public Tarefa ObterPorId(int id) => //Encontrar utilizador pelo ID
            contexto.Tarefas.Find(id);

        public List<Tarefa> getTarefas()
        {
            return contexto.Tarefas.ToList();
        }

        public List<TipoTarefa> getTipoTarefas()
        {
            return contexto.TiposTarefa.ToList();
        }





        public void AvancarTarefa(Tarefa tarefa)
        {
            if (tarefa != null)
            {
                switch (tarefa.EstadoAtual)
                {
                    case EstadoAtual.ToDo:
                        tarefa.EstadoAtual = EstadoAtual.Doing;

                        //FALTA VALIDAÇÕES DOS REQUESITOS PARA PASSAR DE TODO -> DOING
                        break;

                    case EstadoAtual.Doing:
                        tarefa.EstadoAtual = EstadoAtual.Done;
                        //FALTA VALIDAÇÕES DOS REQUESITOS PARA PASSAR DE DOING -> DONE
                        if (tarefa.EstadoAtual == EstadoAtual.ToDo)
                        {
                            MessageBox.Show(
                            "ERRO: Tarefa já está concluída",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        }
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

        //Filtrar tarefas em To-Do
        public List<Tarefa> ObterTarefasToDo()
        {
            return Contexto.Tarefas
                  .Where(tarefa => tarefa.EstadoAtual == EstadoAtual.ToDo)
                  .ToList();
        }

        //Filtrar tarefas em Doing
        public List<Tarefa> ObterTarefasDoing()
        {
            return Contexto.Tarefas
                  .Where(tarefa => tarefa.EstadoAtual == EstadoAtual.Doing)
                  .Include(t => t.TipoTarefa)
                  .Include(t => t.Programador)
                  .Include(t => t.Gestor)
                  .Include(t => t.Projeto)
                  .ToList();
        }

        //Filtrar tarefas em Done
        public List<Tarefa> ObterTarefasDone()
        {
            return Contexto.Tarefas
                  .Where(tarefa => tarefa.EstadoAtual == EstadoAtual.Done)
                  .Include(t => t.TipoTarefa)
                  .Include(t => t.Programador)
                  .Include(t => t.Gestor)
                  .Include(t => t.Projeto)
                  .ToList();
        }

        // Exportar tarefas concluidas para .csv

        public void ExportarTarefasConcluidasParaCsv(string caminhoFicheiro)
        {
            var tarefas = ObterTarefasDone();

            var sb = new StringBuilder();

            // Cabeçalho correto
            sb.AppendLine("Programador,Descricao,DataPrevistaInicio,DataPrevista,TipoTarefa,DataRealInicio,DataRealFim");

            foreach (var tarefa in tarefas)
            {
                sb.AppendLine(
                    $"\"{tarefa.Programador?.Nome}\"," +
                    $"\"{tarefa.Descricao}\"," +
                    $"{(tarefa.DataPrevistaInicio is DateTime dtPrevIni ? dtPrevIni.ToString("yyyy-MM-dd") : "")}," +
                    $"{(tarefa.DataPrevistaFim is DateTime dtPrev ? dtPrev.ToString("yyyy-MM-dd") : "")}," +
                    $"\"{tarefa.TipoTarefa?.Nome}\"," +
                    $"{(tarefa.DataRealInicio is DateTime dtRealIni ? dtRealIni.ToString("yyyy-MM-dd") : "")}," +
                    $"{(tarefa.DataRealFim is DateTime dtRealFim ? dtRealFim.ToString("yyyy-MM-dd") : "")}"
                );
            }

            File.WriteAllText(caminhoFicheiro, sb.ToString(), Encoding.UTF8);
        }


    }
}

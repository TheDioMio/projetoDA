using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

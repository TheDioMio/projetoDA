using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace iTasks.Controllers
{

    /*
     adicionar
    alterar
    apagar

    pesquisas:
    por utilizador
    por username
    devolver todos 


     
     
     */
    public class UtilizadorController
    {
         iTasksContexto contexto = new iTasksContexto(); //Criação de variável global para aceder ao contexto.

        //MODOS DE PROCURA USERS
        //public List<Utilizador> ObterTodos() => //Obter todos os users com o gestor associado
        //    contexto.Utilizadores.Include(utilizadorEncontrado => utilizadorEncontrado.Gestor).ToList();  // Inclui gestor associado

        public List<Programador> ObterProgramadoresDeGestor(Gestor gestor)
        {
            return contexto.Programadores
                .Where(programador => programador.Gestor.Id == gestor.Id)
                .ToList();
        }


        public List<Tarefa> ObterTarefasDeGestor(Gestor gestor)
        {
            return contexto.Tarefas
                .Where(tarefas => tarefas.Gestor.Id == gestor.Id)
                .ToList();
        }

        public List<Tarefa> ObterTarefasDeProgramador(Programador programador)
        {
            return contexto.Tarefas
                .Where(tarefas => tarefas.Programador.Id == programador.Id)
                .ToList();
        }


        public Utilizador ObterPorId(int id) => //Encontrar utilizador pelo ID
            contexto.Utilizadores.Find(id);

        public Utilizador ObterPorUsername(string username) => //Encontrar utilizador pelo USERNAME
            contexto.Utilizadores.FirstOrDefault(utilizadorEncontrado => utilizadorEncontrado.Username == username);
            

        public bool Criar(Utilizador utilizador)
        {
            bool flag = false;
            try
            {
                contexto.Utilizadores.Add(utilizador);
                contexto.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }


        public List<Gestor> GetGestores()
        {
            var gestores = contexto.Utilizadores.OfType<Gestor>().ToList();
            return gestores;
        }

        public List<Programador> GetProgramadores()
        {
            var programadores = contexto.Utilizadores.OfType<Programador>().ToList();
            return programadores;
        }

        public bool Atualizar(Utilizador utilizadorEncontrado)
        {
            bool flag = false;
            try
            {
                contexto.Entry(utilizadorEncontrado).State = EntityState.Modified;
                contexto.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;  
        }


        //Eliminar utilizadores
        public bool Eliminar(int id)
        {
            bool flag = false;
            try
            {
                var utilizadorEncontrado = contexto.Utilizadores.Find(id);
                if (utilizadorEncontrado != null)
                {
                    contexto.Utilizadores.Remove(utilizadorEncontrado);
                    contexto.SaveChanges();
                    flag = true;
                }
               
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
    }
}

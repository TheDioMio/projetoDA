using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks.Data;
using iTasks.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;


namespace iTasks.Controllers
{
    public class UtilizadorController
    {
         iTasksContexto contexto = new iTasksContexto(); //Criação de variável global para aceder ao contexto.

        //MODOS DE PROCURA USERS
        public List<Utilizador> ObterTodos() => //Obter todos os users com o gestor associado
            contexto.Utilizadores.Include(utilizadorEncontrado => utilizadorEncontrado.Gestor).ToList();  // Inclui gestor associado

        public Utilizador ObterPorId(int id) => //Encontrar utilizador pelo ID
            contexto.Utilizadores.Find(id);

        public Utilizador ObterPorUsername(string username) => //Encontrar utilizador pelo USERNAME
            contexto.Utilizadores.FirstOrDefault(utilizadorEncontrado => utilizadorEncontrado.Username == username);

        public void Criar(Utilizador utilizadorEncontrado)
        {
            contexto.Utilizadores.Add(utilizadorEncontrado);
            contexto.SaveChanges();
        }

        public void Atualizar(Utilizador utilizadorEncontrado)
        {
            contexto.Entry(utilizadorEncontrado).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        //Eliminar utilizadores
        public void Eliminar(int id)
        {
            var utilizadorEncontrado = contexto.Utilizadores.Find(id);
            if (utilizadorEncontrado != null)
            {
                contexto.Utilizadores.Remove(utilizadorEncontrado);
                contexto.SaveChanges();
            }
        }
    }
}

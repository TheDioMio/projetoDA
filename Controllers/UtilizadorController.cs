using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks.Data;
using iTasks.Models;
using System.Data.Entity;


namespace iTasks.Controllers
{
    internal class UtilizadorController
    {
        private readonly iTasksContexto _ctx = new iTasksContexto();

        public List<Utilizador> ObterTodos() =>
            _ctx.Utilizadores.Include(u => u.Gestor).ToList();  // Inclui gestor associado

        public Utilizador ObterPorId(int id) =>
            _ctx.Utilizadores.Find(id);

        public void Criar(Utilizador u)
        {
            _ctx.Utilizadores.Add(u);
            _ctx.SaveChanges();
        }

        public void Atualizar(Utilizador u)
        {
            _ctx.Entry(u).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var u = _ctx.Utilizadores.Find(id);
            if (u != null)
            {
                _ctx.Utilizadores.Remove(u);
                _ctx.SaveChanges();
            }
        }
    }
}

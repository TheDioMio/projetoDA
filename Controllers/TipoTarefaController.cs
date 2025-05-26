using iTasks.Data;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controllers
{
    class TipoTarefaController
    {
        public List<TipoTarefa> ObterTodos()
        {
            using (var contexto = new iTasksContexto())
            {
                return contexto.TiposTarefa.ToList();
            }
        }

        public void Adicionar(string nome)
        {
            using (var contexto = new iTasksContexto())
            {
                var tipo = new TipoTarefa { Nome = nome };
                contexto.TiposTarefa.Add(tipo);
                contexto.SaveChanges();
            }
        }

        public void Editar(int id, string novoNome)
        {
            using (var contexto = new iTasksContexto())
            {
                var tipo = contexto.TiposTarefa.Find(id);
                if (tipo != null)
                {
                    tipo.Nome = novoNome;
                    contexto.SaveChanges();
                }
            }
        }

        public void Remover (int id)
        {

            using (var contexto = new iTasksContexto())
            {
                var tipo = contexto.TiposTarefa.Find(id);

                if (tipo != null)
                {
                    contexto.TiposTarefa.Remove(tipo);
                    contexto.SaveChanges();
                }
            }

        }

        public bool NomeExiste(string nome)
        {
            using (var contexto = new iTasksContexto())
            {
                return contexto.TiposTarefa.Any(t => t.Nome == nome);
            }
        }

        public bool NomeExisteParaOutroId(string nome, int id)
        {
            using (var contexto = new iTasksContexto())
            {
                return contexto.TiposTarefa.Any(t => t.Nome == nome && t.Id != id);
            }
        }
    }
}

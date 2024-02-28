using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ContatoRepositorio(BancoContext bancoContext) {

            _bancoContext = bancoContext;

        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();

            return contato;

        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            var contatoDB = ListarPorId(contato.ID);

            if (contatoDB == null) throw new Exception("Houve um erro na atualização do contato.");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Telefone = contato.Telefone;

            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }

        public bool Apagar(int id)
        {
            var contatoDel = ListarPorId(id);

            if (contatoDel == null) throw new Exception("Houve um erro para deletar");

            _bancoContext.Contatos.Remove(contatoDel);
            _bancoContext.SaveChanges();

            return true;

        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.ID == id);
        }

    }
}

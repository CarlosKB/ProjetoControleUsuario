using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {

        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio) {

            _contatoRepositorio = contatoRepositorio;

        }
        public IActionResult Index()
        {

            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();

            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            var contatoMocado = _contatoRepositorio.ListarPorId(id);

            return View(contatoMocado);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            var contatoView = _contatoRepositorio.ListarPorId(id);

            return View(contatoView);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                var apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato deletado com sucesso.";

                }
                else
                {
                    TempData["MensagemErro"] = "Ops, erro ao deletar seu contato.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos deletar seu contato, detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = $"Contato: {contato.Nome} alterado com sucesso.";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu contato, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

    }
}

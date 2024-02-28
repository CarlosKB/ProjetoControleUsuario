using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            var sessaoUsuario = _sessao.BuscarSessaoUsuario();

            if(sessaoUsuario != null) {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {

                if(ModelState.IsValid)
                {
                    UsuarioModel usuarioLogin = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuarioLogin != null)
                    {
                        if(usuarioLogin.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuarioLogin);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do usuário é inválida, tente novamente.";
                    }
                    else
                    {
                       TempData["MensagemErro"] = $"Login e/ou senha inválido(s).";
                    }
                }

                return View("Index");
                

            }catch (Exception error)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

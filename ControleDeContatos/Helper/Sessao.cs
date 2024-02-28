using ControleDeContatos.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ControleDeContatos.Helper
{
    public class Sessao : ISessao
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public Sessao(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public UsuarioModel BuscarSessaoUsuario()
        {
            string sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

        }


        public void CriarSessaoUsuario(UsuarioModel usuario)
        {
        string valorJson = JsonSerializer.Serialize(usuario);  
        _contextAccessor.HttpContext.Session.SetString("SessaoUsuarioLogado", valorJson);
        }


        public void RemoverSessaoUsuario()
        {
            _contextAccessor.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
    }
}

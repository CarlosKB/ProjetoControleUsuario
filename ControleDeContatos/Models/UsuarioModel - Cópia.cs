using ControleDeContatos.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome de usuário é obrigatorio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Login de usuário é obrigatorio.")]
        public string Login { get; set;}

        [Required(ErrorMessage = "Email de usuário é obrigatorio.")]
        [EmailAddress(ErrorMessage = "O email informado não é valido.")]
        public string Email { get; set;}

        [Required(ErrorMessage = "Perfil de usuário é obrigatorio.")]
        public PerfilEnum Perfil { get; set;}

        [Required(ErrorMessage = "Senha de usuário é obrigatorio.")]
        public string Senha { get; set;}
        public DateTime DataCadastro { get; set;}
        public DateTime? DataAtualizacao { get; set;}
    }
}

using Ashow.Business;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Ashow.Domain.Model
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Senha { get; set; }
        public string? Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public void ValidarCamposDeCadastro()
        {
            ValidaNome();
            ValidaSenha();
            ValidaEmail();
        }
        public void ValidaNome()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new Exception("Preencha o campo de Usuário");
            }
            if (Nome.Length < 3)
            {
                throw new Exception("Usuário deve ter no mínimo 4 caracteres");
            }
        }
        public void ValidaSenha()
        {
            if (string.IsNullOrEmpty(Senha))
            {
                throw new Exception("Preencha o campo de Senha");
            }
            if (Nome.Length < 7)
            {
                throw new Exception("Usuário deve ter no mínimo 8 caracteres");
            }
        }
        public void ValidaEmail()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception("Preencha o campo de Email");
            }

            string regexEmail = @"^[\w.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            bool emailValido = Regex.IsMatch(Email, regexEmail);

            if (!emailValido)
            {
                throw new Exception("Email inválido.");
            }
        }
        public void TransformaSenhaEmHash()
        {
            var usuarioModel = new UsuarioModel();
            var hasher = new PasswordHasher<UsuarioModel>();
            string senhaHash = hasher.HashPassword(usuarioModel, Senha);
            Senha = senhaHash;
        }
    }
}

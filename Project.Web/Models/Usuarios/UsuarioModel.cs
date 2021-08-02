using Project.Web.Models.Pessoas;

namespace Project.Web.Models.Usuarios
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {
            Pessoa = new PessoaModel();
        }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }

        public virtual PessoaModel Pessoa { get; set; }
    }
}

using Project.Web.Models.Usuarios;

using System;

namespace Project.Web.Models.Pessoas
{
    public class PessoaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
    }
}
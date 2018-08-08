using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedeCidada.Models
{
    public class UsuarioModel
    {

        public int Id { get; set; }

        [Required]
        [Display(Name ="Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
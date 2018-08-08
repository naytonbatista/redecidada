using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedeCidada.Models
{
    public class ProfessorModel
    {

        public int Id { get; set; }

        [Required]
        [Display(Name ="Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Polo")]
        public string Polo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
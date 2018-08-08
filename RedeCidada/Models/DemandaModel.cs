using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedeCidada.Models
{
    public class DemandaModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nome")]
        public string Nome { get; set; }
        
    }
}
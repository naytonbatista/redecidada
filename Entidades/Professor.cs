using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Professor
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Polo { get; set; }
        
        public DateTime DataCadastro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Matricula
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }

        public DateTime DataNascimento{ get; set; }

        public string Naturalidade { get; set; }

        public string Telefone { get; set; }
        
        public string NomePai { get; set; }

        public string NomeMae { get; set; }

        public int Demanda { get; set; }

        public string Endereco { get; set; }

        public string Escola { get; set; }

        public int Professor { get; set; }

        public string  Nucleo{ get; set; }

        public string Observacoes { get; set; }

        public int Turno { get; set; }

        public string Serie { get; set; }
                
        public int Municipio { get; set; }
        
        public DateTime DataCadastro { get; set; }

        public List<int> Atividades
        {
            get
            {
                return _atividades;
            }

            set
            {
                _atividades = value;
            }
        }
        
        private List<int> _atividades = new List<int>();


    }
}

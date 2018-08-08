using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;

namespace RedeCidada.Models
{
    public class MatriculaModel
    {
        public MatriculaModel(List<Demanda> demandas, List<Professor> professores, List<Atividade> atividades, List<Municipio> municipios, List<Estado> estados, List<Turno> turnos)
        {

            LstDemanda = demandas;

            LstAtividades = atividades;

            LstProfessores = new SelectList(professores, "Id", "Nome");
            
            LstEstados = new SelectList(estados, "Id", "Nome");
            
            LstMunicipios = new SelectList(municipios, "Id", "Nome");

            LstTurnos = new SelectList(turnos, "Id", "Nome");
            
        }

        public MatriculaModel()
        {
            LstDemanda = new List<Entidades.Demanda>();

            LstAtividades = new List<Atividade>();
                        
            LstProfessores = new SelectList(new string[] { });
            
            LstMunicipios = new SelectList(new string[] { });
            
            LstEstados = new SelectList(new string[] { });
            

        }

        public int Id { get; set; }

        [Required(ErrorMessage ="O Campo Nome do Aluno é obrigatório.")]
        [Display(Name ="Nome do Aluno")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Campo Data de Nascimento é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Naturalidade")]
        public string Naturalidade { get; set; }

        [Display(Name = "Telefone")]
        [Phone]
        public string Telefone { get; set; }

        [Display(Name = "Nome do Pai")]
        public string NomePai { get; set; }

        [Display(Name = "Nome da Mãe")]
        public string NomeMae { get; set; }

        [Required(ErrorMessage = "Selecione uma demanda.")]
        [Display(Name = "Demanda")]
        public int? Demanda { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Escola")]
        public string Escola { get; set; }

        [Required(ErrorMessage = "O Campo Professor é obrigatório.")]
        [Display(Name = "Professor")]
        public int? Professor { get; set; }
        
        private List<int> _atividades = new List<int>();

        [Required(ErrorMessage = "Selecione pelo menos uma ATIVIDADE.")]
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
                
        [Required(ErrorMessage = "O campo município é obrigatório.")]
        public int? Municipio { get; set; }

        [Required(ErrorMessage = "O campo estado é obrigatório.")]
        public int? Estado { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "O campo turno é obrigatório.")]
        public int Turno { get; set; }

        [Display(Name = "Serie")]
        public string Serie { get; set; }

        [Display(Name = "Observacoes")]
        public string Observacoes { get; set; }

        [Display(Name = "Núcleo")]
        public string Nucleo { get; set; }
        
        public DateTime DataCadastro { get; set; }

        #region Listas

        private List<Entidades.Demanda> _lstDemanda;

        private List<Atividade> _lstAtividades;

        private SelectList _lstMunicipio;

        private SelectList _lstTurno;

        private SelectList _lstEstado;

        private SelectList _lstProfessores;
        
        public SelectList LstProfessores
        {
            get
            {
                return _lstProfessores;
            }

            set
            {
                _lstProfessores = value;
            }
        }

        public List<Demanda> LstDemanda
        {
            get
            {
                return _lstDemanda;
            }

            set
            {
                _lstDemanda = value;
            }
        }

        public List<Atividade> LstAtividades
        {
            get
            {
                return _lstAtividades;
            }

            set
            {
                _lstAtividades = value;
            }
        }

        public SelectList LstMunicipios
        {
            get { return _lstMunicipio;}
            set { _lstMunicipio = value; }
        }

        public SelectList LstEstados
        {
            get { return _lstEstado; }
            set { _lstEstado = value; }
        }

        public SelectList LstTurnos { get; set; }
        #endregion
    }
}
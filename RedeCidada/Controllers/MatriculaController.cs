using RedeCidada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MatriculaRepository = Repository.Matricula;
using Entidades;

namespace RedeCidada.Controllers
{
    public class MatriculaController : Controller
    {
        private Usuario UsuarioLogado
        {
            get { return Session["usuario"] == null ? null : (Usuario)Session["usuario"]; }
        }

        private MatriculaRepository _repository = null;
        private Repository.Demanda _repositoryDemanda = null;
        private Repository.Atividade _repositoryAtividade = null;
        private Repository.Professor _repositoryProfessor = null;
        private Repository.Estado _repositoryEstado = null;
        private Repository.Municipio _repositoryMunicipio = null;
        private Repository.Turno _repositoryTurno = null;
        
        public MatriculaController()
        {
            _repository = new MatriculaRepository();
            _repositoryDemanda = new Repository.Demanda();
            _repositoryAtividade = new Repository.Atividade();
            _repositoryProfessor = new Repository.Professor();
            _repositoryEstado = new Repository.Estado();
            _repositoryMunicipio = new Repository.Municipio();
            _repositoryTurno = new Repository.Turno();

        }

        public ActionResult Index()
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            List<Matricula> matriculas = _repository.Listar();
            return View(matriculas);
        }

        public ActionResult Cadastrar()
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            var model = new MatriculaModel(
                _repositoryDemanda.Listar(), 
                _repositoryProfessor.Listar(), 
                _repositoryAtividade.ListarAtividades(), 
                new List<Municipio>(), 
                _repositoryEstado.Listar(), _repositoryTurno.Listar());

            return View(model);
        }
        
        [HttpPost]
        public ActionResult Cadastrar(MatriculaModel model)
        {
            try
            {               
                
                if (UsuarioLogado == null)
                {
                    return RedirectToAction("Login", "Inicio");
                }

                var returnModel = new MatriculaModel(
                                    _repositoryDemanda.Listar(),
                                    _repositoryProfessor.Listar(),
                                    _repositoryAtividade.ListarAtividades(),
                                    _repositoryMunicipio.Listar(model.Estado.GetValueOrDefault()), 
                                    _repositoryEstado.Listar(), 
                                    _repositoryTurno.Listar());
                

                if (model.Atividades == null || model.Atividades.Count <= 0)
                {
                    ViewBag.Mensagem = "Campo atividades é obrigatório.";
                    return View(returnModel);
                }

                if (!ModelState.IsValid)
                {
                    return View(returnModel);
                }
              
                _repository.Cadastrar(new Matricula
                {
                    Nome = model.Nome,
                    DataNascimento = model.DataNascimento.GetValueOrDefault(),
                    Telefone = model.Telefone,
                    Demanda = model.Demanda.GetValueOrDefault(),
                    Endereco = model.Endereco,
                    Escola = model.Escola,
                    Naturalidade = model.Naturalidade,
                    NomeMae = model.NomeMae,
                    NomePai = model.NomePai,
                    Nucleo = model.Nucleo,
                    Atividades = model.Atividades,
                    Professor = model.Professor.GetValueOrDefault(),
                    Municipio = model.Municipio.GetValueOrDefault(),
                    Observacoes = model.Observacoes,
                    Turno = model.Turno,
                    Serie = model.Serie
                });

                ModelState.Clear();

                ViewBag.Mensagem = "Matrícula cadastrada com sucesso!";
                return View(new MatriculaModel(_repositoryDemanda.Listar(), _repositoryProfessor.Listar(), _repositoryAtividade.ListarAtividades(), new List<Municipio>(), _repositoryEstado.Listar(), _repositoryTurno.Listar()));


            }
            catch (Exception exc)
            {

                ViewBag.Mensagem = "ERRO - " + exc.Message;
                ModelState.Clear();
                return View(new MatriculaModel(_repositoryDemanda.Listar(), _repositoryProfessor.Listar(), _repositoryAtividade.ListarAtividades(), new List<Municipio>(), _repositoryEstado.Listar(), _repositoryTurno.Listar()));

            }

        }

        public ActionResult Visualizar(int id)
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            var matricula = _repository.Listar().First(x => x.Id == id);

            var estado = _repositoryEstado.BuscarPorMunicipio(matricula.Municipio);

            matricula.Atividades = _repositoryAtividade.ListarAtividades(id);

            var model = new MatriculaModel(_repositoryDemanda.Listar(), _repositoryProfessor.Listar(), _repositoryAtividade.ListarAtividades(), _repositoryMunicipio.Listar(estado.GetValueOrDefault()), _repositoryEstado.Listar(), _repositoryTurno.Listar())
            {
                Id = id,
                Atividades = matricula.Atividades,
                DataCadastro = matricula.DataCadastro,
                DataNascimento = matricula.DataNascimento, 
                Demanda = matricula.Demanda,
                Endereco = matricula.Endereco,
                Escola = matricula.Escola,
                Naturalidade = matricula.Naturalidade,
                Nome = matricula.Nome,
                NomeMae = matricula.NomeMae,
                NomePai = matricula.NomePai,
                Nucleo = matricula.Nucleo,
                Professor = matricula.Professor,
                Telefone = matricula.Telefone,
                Estado =  estado,
                Observacoes = matricula.Observacoes,
                Turno = matricula.Turno,
                Serie = matricula.Serie,
                Municipio = matricula.Municipio
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult ObterMunicipios(int estadoId)
        {
            try
            {
                var retorno = _repositoryMunicipio.Listar(estadoId);

                return Json(new { Success = true, lista = retorno }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Editar(int id)
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            var matricula = _repository.Listar().First(x => x.Id == id);

            var estado = _repositoryEstado.BuscarPorMunicipio(matricula.Municipio);

            matricula.Atividades = _repositoryAtividade.ListarAtividades(id);

            var model = new MatriculaModel(_repositoryDemanda.Listar(), _repositoryProfessor.Listar(), _repositoryAtividade.ListarAtividades(), _repositoryMunicipio.Listar(estado.GetValueOrDefault()), _repositoryEstado.Listar(), _repositoryTurno.Listar())
            {
                Id = id,
                Atividades = matricula.Atividades,
                DataCadastro = matricula.DataCadastro,
                DataNascimento = matricula.DataNascimento,
                Demanda = matricula.Demanda,
                Endereco = matricula.Endereco,
                Escola = matricula.Escola,
                Naturalidade = matricula.Naturalidade,
                Nome = matricula.Nome,
                NomeMae = matricula.NomeMae,
                NomePai = matricula.NomePai,
                Nucleo = matricula.Nucleo,
                Professor = matricula.Professor,
                Telefone = matricula.Telefone,
                Estado = estado,
                Observacoes = matricula.Observacoes,
                Turno = matricula.Turno,
                Serie = matricula.Serie,
                Municipio = matricula.Municipio
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(MatriculaModel model)
        {
            try
            {

                if (UsuarioLogado == null)
                {
                    return RedirectToAction("Login", "Inicio");
                }

                var returnModel = new MatriculaModel(
                                    _repositoryDemanda.Listar(),
                                    _repositoryProfessor.Listar(),
                                    _repositoryAtividade.ListarAtividades(),
                                    _repositoryMunicipio.Listar(model.Estado.GetValueOrDefault()),
                                    _repositoryEstado.Listar(),
                                    _repositoryTurno.Listar());


                if (model.Atividades == null || model.Atividades.Count <= 0)
                {
                    ViewBag.Mensagem = "Campo atividades é obrigatório.";
                    return View(returnModel);
                }

                if (!ModelState.IsValid)
                {
                    return View(returnModel);
                }

                _repository.Cadastrar(new Matricula
                {
                    Nome = model.Nome,
                    DataNascimento = model.DataNascimento.GetValueOrDefault(),
                    Telefone = model.Telefone,
                    Demanda = model.Demanda.GetValueOrDefault(),
                    Endereco = model.Endereco,
                    Escola = model.Escola,
                    Naturalidade = model.Naturalidade,
                    NomeMae = model.NomeMae,
                    NomePai = model.NomePai,
                    Nucleo = model.Nucleo,
                    Atividades = model.Atividades,
                    Professor = model.Professor.GetValueOrDefault(),
                    Municipio = model.Municipio.GetValueOrDefault(),
                    Observacoes = model.Observacoes,
                    Turno = model.Turno,
                    Serie = model.Serie
                });

                ModelState.Clear();

                ViewBag.Mensagem = "Matrícula editada com sucesso!";
                return View(new MatriculaModel(_repositoryDemanda.Listar(), _repositoryProfessor.Listar(), _repositoryAtividade.ListarAtividades(), new List<Municipio>(), _repositoryEstado.Listar(), _repositoryTurno.Listar()));


            }
            catch (Exception exc)
            {

                ViewBag.Mensagem = "ERRO - " + exc.Message;
                ModelState.Clear();
                return View(new MatriculaModel(_repositoryDemanda.Listar(), _repositoryProfessor.Listar(), _repositoryAtividade.ListarAtividades(), new List<Municipio>(), _repositoryEstado.Listar(), _repositoryTurno.Listar()));

            }
        }

    }
}
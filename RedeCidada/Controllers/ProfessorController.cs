using RedeCidada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfessorRepository = Repository.Professor;
using Entidades;

namespace RedeCidada.Controllers
{
    public class ProfessorController : Controller
    {
        private String UsuarioLogado
        {
            get { return Session["usuario_admin"] == null ? null : Session["usuario_admin"].ToString(); }
        }

        ProfessorRepository _repository = null;

        public ProfessorController()
        {
            _repository = new ProfessorRepository();
        }
                
        public ActionResult Index()
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            List<Professor> professor = _repository.Listar();
            
            return View(professor);
        }

        public ActionResult Cadastrar()
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(ProfessorModel model)
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }
            _repository.Cadastrar(new Professor { Nome = model.Nome, Polo = model.Polo });

            ModelState.Clear();

            ViewBag.Mensagem = "Professor cadastrado com sucesso!";

            return View();
        }
    }
}
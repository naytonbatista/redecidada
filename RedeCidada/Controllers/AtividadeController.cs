using RedeCidada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtividadeRepository = Repository.Atividade;
using Entidades;

namespace RedeCidada.Controllers
{
    public class AtividadeController : Controller
    {
        private String UsuarioLogado
        {
            get { return Session["usuario_admin"] == null ? null : Session["usuario_admin"].ToString(); }
        }

        private AtividadeRepository _repository = null;

        public AtividadeController()
        {
            _repository = new AtividadeRepository();
        }
                
        public ActionResult Index()
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            List<Atividade> atividades = _repository.ListarAtividades();
            return View(atividades);
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
        public ActionResult Cadastrar(AtividadeModel model)
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }
            _repository.Cadastrar(new Atividade { Nome = model.Nome });

            ModelState.Clear();

            ViewBag.Mensagem = "Atividade cadastrada com sucesso!";

            return View();
        }
    }
}
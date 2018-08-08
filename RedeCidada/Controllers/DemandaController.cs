using RedeCidada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemandaRepository = Repository.Demanda;
using Entidades;

namespace RedeCidada.Controllers
{
    public class DemandaController : Controller
    {
        private String UsuarioLogado
        {
            get { return Session["usuario_admin"] == null ? null : Session["usuario_admin"].ToString(); }
        }

        private DemandaRepository _repository = null;

        public DemandaController()
        {
            _repository = new DemandaRepository();
        }
                
        public ActionResult Index()
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            List<Demanda> demandas = _repository.Listar();
            return View(demandas);
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
        public ActionResult Cadastrar(DemandaModel model)
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }
            _repository.Cadastrar(new Demanda { Nome = model.Nome });

            ModelState.Clear();

            ViewBag.Mensagem = "Demanda cadastrada com sucesso!";

            return View();
        }
    }
}
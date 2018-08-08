using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryUsuario = Repository.Usuario;
using Entidades;
using RedeCidada.Models;
using System.Security.Cryptography;
using System.Text;

namespace RedeCidada.Controllers
{
    public class UsuarioController : Controller
    {
        private String UsuarioLogado
        {
            get { return Session["usuario_admin"] == null ? null : Session["usuario_admin"].ToString(); }
        }

        RepositoryUsuario _repository = null;

        public UsuarioController()
        {
            _repository = new RepositoryUsuario();
        }

        // GET: Usuario
        public ActionResult Index()
        {

            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            List<Usuario> usuarios = _repository.Listar();

            return View(usuarios);
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
        public ActionResult Cadastrar(UsuarioModel model)
        {
            if (UsuarioLogado == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            StringBuilder senha = new StringBuilder();
            foreach (byte valor in MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(model.Senha)))
            {
                senha.Append(valor.ToString("X2"));
            }

            _repository.Cadastrar(new Usuario { Nome = model.Nome, Login = model.Login, Senha = senha.ToString() });

            ModelState.Clear();

            ViewBag.Mensagem = "Usuário cadastrado com sucesso!";

            return View();
        }
    }
}
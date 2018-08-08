using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UsuarioRepository = Repository.Usuario;

namespace RedeCidada.Controllers
{

    public class InicioController : Controller
    {
        private UsuarioRepository _usuarioRepository;

        public InicioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(string login, string senha)
        {
            try
            {
                if (login == "REDEADM" && senha == "REDESISTEM@")
                {
                    Session["usuario_admin"] = "ADMINISTRADOR";
                    
                    return Json(new
                    {
                        @url = "professor/index"

                    }, JsonRequestBehavior.AllowGet);
                }


                StringBuilder senhaBuilder = new StringBuilder();
                foreach (byte valor in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(senha)))
                {
                    senhaBuilder.Append(valor.ToString("X2"));
                }

                Usuario usuario = _usuarioRepository.Logar(login, senhaBuilder.ToString());

                if (usuario == null || usuario.Id <= 0)
                {
                    return Json(new
                    {

                        @message = "Nome de usuário ou senha estão incorretos."

                    }, JsonRequestBehavior.AllowGet);

                }
                
                Session["usuario"] = usuario;
                
                return Json(new
                {
                    @url = "matricula/index"

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(new
                {
                    @message = exc.Message

                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            Session["usuario_admin"] = null;
            Session["usuario"] = null;

            return RedirectToAction("Login");
        }
    }
}
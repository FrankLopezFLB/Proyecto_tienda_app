using Proyecto_tienda_app.Models;
using Proyecto_tienda_app.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_tienda_app.DAO
{
    public class UsuarioController : Controller
    {
        usuarioDAO usuario = new usuarioDAO();

        // GET
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            return View(usuario);
        }

        public ActionResult Listado()
        {
            List<Usuario> listado = new List<Usuario>();

            return View();
        }

        // GET
        public ActionResult Actualizar(int codigo)
        {

            // Se busca un cliente
            Usuario usuario = new Usuario();

            return View(usuario);
        }

        public ActionResult Actualizar(int codigo, Usuario usuario)
        {
            return View(usuario);
        }

        public ActionResult Eliminar(int codigo)
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string clave)
        {

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no es correcta";
                return View();
            }

            Session["Usuario"] = usuario;

            return RedirectToAction("Index", "Cliente");
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }

    }
}
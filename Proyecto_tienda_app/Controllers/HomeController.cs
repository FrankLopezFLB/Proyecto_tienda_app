using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Proyecto_tienda_app.Models;
using Proyecto_tienda_app.DAO;

namespace Proyecto_tienda_app.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 1)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Tienda", "Ecommerce");
                }
            }

            return View();
        }

        public ActionResult About()
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 1)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Tienda", "Ecommerce");
                }
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 1)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Tienda", "Ecommerce");
                }
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
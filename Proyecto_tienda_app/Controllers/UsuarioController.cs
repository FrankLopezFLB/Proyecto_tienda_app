using Proyecto_tienda_app.Models;
using Proyecto_tienda_app.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Proyecto_tienda_app.DAO
{
    public class UsuarioController : Controller
    {
        usuarioDAO usuario = new usuarioDAO();      

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

        public ActionResult Login(string message="")
        {
            ViewBag.MESSAGE = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Clave)
        {
            Usuario usuario = usuarioDAO.Instancia.Login(Email, Clave);

            if (usuario == null)
            {
                return Login("Usuario o contraseña no es correcta");
            }

            Session["Usuario"] = usuario;

            if (usuario.puestoID == 2)
            {                
                return RedirectToAction("Tienda", "Ecommerce");
            }            
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Usuario");
        }
        public ActionResult Registrar()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@nombre",Value=usuario.Nombre},
                new SqlParameter(){ParameterName="@apellido",Value=usuario.Apellido},
                new SqlParameter(){ParameterName="@telefono",Value=usuario.Telefono},
                new SqlParameter(){ParameterName="@direccion",Value=usuario.Direccion},
                new SqlParameter(){ParameterName="@email",Value=usuario.Email},
                new SqlParameter(){ParameterName="@clave",Value=usuario.Clave},
                new SqlParameter(){ParameterName="@dni",Value=usuario.Dni}
            };
            ViewBag.mensaje = usuarioDAO.Instancia.CRUD("sp_createUser", pars, 1);
            return View(usuario);
        }

    }
}
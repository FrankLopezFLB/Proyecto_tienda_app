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
        PuestoDAO daoP = new PuestoDAO();

        public ActionResult Listado()
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
            return View(usuario.listadoUsuarios());
        }

        // GET
        public ActionResult Actualizar(int codigo)
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
            Usuario us = usuario.Buscar(codigo);
            ViewBag.puestos = new SelectList(daoP.Listado(), "Id", "Nombre", us.puestoID);
            return View(us);
        }

        [HttpPost]
        public ActionResult Actualizar(Usuario usu)
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
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@cod",Value=usu.Codigo},
                new SqlParameter(){ParameterName="@nom",Value=usu.Nombre},
                new SqlParameter(){ParameterName="@ape",Value=usu.Apellido},
                new SqlParameter(){ParameterName="@tel",Value=usu.Telefono},
                new SqlParameter(){ParameterName="@dir",Value=usu.Direccion},
                new SqlParameter(){ParameterName="@ema",Value=usu.Email},
                new SqlParameter(){ParameterName="@cla",Value=usu.Clave},
                new SqlParameter(){ParameterName="@dni",Value=usu.Dni},
                new SqlParameter(){ParameterName="@pue",Value=usu.puestoID},
            };

            ViewBag.mensaje = usuarioDAO.Instancia.CRUD("sp_alfredo_actualizarUsuario", pars, 2);
            ViewBag.puestos = new SelectList(daoP.Listado(), "Id", "Nombre",usu.puestoID);
            ViewBag.confirmacion = "Usuario Actualizado";
            return View(usu);
        }

        public ActionResult Eliminar(int codigo)
        {
            return View();
        }

        public ActionResult Login(string message="")
        {
            // Verifica si ya inició sesión
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario != null)
            {
                if (existeUsuario.puestoID == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Tienda", "Ecommerce");
                }
            }

            ViewBag.MESSAGE = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Clave)
        {
            // Verifica si ya inició sesión

            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario != null)
            {
                if (existeUsuario.puestoID == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Tienda", "Ecommerce");
                }
            }

            if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Clave))
            {
                return Login("Se requiere email y clave");
            }

            Usuario usuario = usuarioDAO.Instancia.Login(Email, Clave);

            if (usuario == null)
            {
                return Login("Usuario o contraseña no es correcta");
            }

            Session["usuario"] = usuario;

            if (usuario.puestoID == 2)
            {
                return RedirectToAction("Tienda", "Ecommerce");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult Registrar()
        {
            // Verifica si ya inició sesión

            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario != null)
            {
                if (existeUsuario.puestoID == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Tienda", "Ecommerce");
                }
            }

            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            // Verifica si ya inició sesión
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario != null)
            {
                if (existeUsuario.puestoID == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Tienda", "Ecommerce");
                }
            }

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
            ViewBag.marmota = "Usuario Agregado";
            return View(usuario);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Proyecto_tienda_app.Models;
using Proyecto_tienda_app.DAO;

namespace Proyecto_tienda_app.Controllers
{
    public class EcommerceController : Controller
    {
        productosDAO productoDAO = new productosDAO();
        ventaDAO ventasDao = new ventaDAO();

        // GET: Ecommerce
        public ActionResult Tienda(string nombre = "")
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if(existeUsuario == null)
            {
                return RedirectToAction("Login","Usuario");
            }else
            {
                if (existeUsuario.puestoID == 2)
                {
                    ViewBag.USUARIO = existeUsuario;
                } else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<Item>();
            }

            var filtrado = productoDAO.Filtro(nombre);

            if (filtrado.Count() == 0)
            {
                ViewBag.MENSAJE = $"No se encontró coincidencias con la palabra {nombre}";
            }

            return View(filtrado);
        }

        public ActionResult Seleccionar(int id = 0)
        {
            ViewBag.error = false;

            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 2)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            Producto reg = productoDAO.Buscar(id);
            if (reg == null)
            {
                return RedirectToAction("Tienda", new { nombre = "" });
            }

            ViewBag.d = (Session["carrito"] as List<Item>).Exists(p => p.codigo == id) ? true : false;
            ViewBag.mensaje = "Producto ya esta registrado en el carrito";

            return View(reg);
        }

        [HttpPost]
        public ActionResult Seleccionar(int codigo, int cantidad)
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 2)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            Producto reg = productoDAO.Buscar(codigo);

            if (cantidad > reg.stock)
            {
                ViewBag.mensaje = $"El producto {reg.descripcion} solo dispone de {reg.stock}";
                ViewBag.error = true;
                ViewBag.d = false;
                return View(reg);
            }

            Item it = new Item()
            {
                codigo = codigo,
                descripcion = reg.descripcion,
                precio = reg.precio,
                cantidad = cantidad
            };

            (Session["carrito"] as List<Item>).Add(it);
            ViewBag.d = true;
            ViewBag.error = false;

            ViewBag.mensaje = "Producto Agregado";
            return View(reg);
        }

        public ActionResult Carrito()
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 2)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.listado = productoDAO.listado();

            if (Session["carrito"] == null)
            {
                return RedirectToAction("Tienda", new { nombre = "" });
            }

            return View(Session["carrito"] as List<Item>);
        }

        [HttpPost]
        public ActionResult Actualizar(int id, int q)
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 2)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            Item reg = (Session["carrito"] as List<Item>).Where(p => p.codigo == id).FirstOrDefault();

            reg.cantidad = q;

            ViewBag.listado = productoDAO.listado();

            return RedirectToAction("Carrito");
        }

        public ActionResult Delete(int id)
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 2)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            Item reg = (Session["carrito"] as List<Item>).Find(p => p.codigo == id);

            (Session["carrito"] as List<Item>).Remove(reg);

            ViewBag.listado = productoDAO.listado();

            return RedirectToAction("Carrito");
        }

        public ActionResult Aviso(string m)
        {
            var existeUsuario = Session["usuario"] as Usuario;

            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                if (existeUsuario.puestoID == 2)
                {
                    ViewBag.USUARIO = existeUsuario;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.MENSAJE = m;
            return View();
        }

        public ActionResult Comprar()
        {
            var existeUsuario = Session["usuario"] as Usuario;

            // TODO: Obtener a cliente o usuario por la session
            if (existeUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            if (existeUsuario.puestoID != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.USUARIO = existeUsuario;

            var lista = (Session["carrito"] as List<Item>);

            if (lista.Count == 0)
            {
                ViewBag.listado = productoDAO.listado();
                return RedirectToAction("Carrito");
            }

            Boleta boleta = new Boleta
            {
                Codigo = 0, // no es obligatorio
                CodigoCliente = existeUsuario.Codigo, // se requiere sesion
                Fecha = DateTime.Now, // El procedure se encarga
                PrecioTotal = lista.Sum(i => i.monto)
            };

            Response response = ventasDao.RegistrarCompras(boleta, lista);

            if(response.HayError)
            {
                ViewBag.listado = productoDAO.listado();
                ViewBag.mensajeError = response.Mensaje;
                return RedirectToAction("Aviso", new { m = response.Mensaje });
            }

            Session["carrito"] = null;

            return RedirectToAction("Aviso", new { m = response.Mensaje });

        }


    }
}
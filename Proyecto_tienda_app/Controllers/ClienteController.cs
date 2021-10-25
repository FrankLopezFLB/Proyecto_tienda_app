using Proyecto_tienda_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_tienda_app.DAO
{
    public class ClienteController : Controller
    {
        clienteDAO cliente = new clienteDAO();

        // GET
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Cliente cliente)
        {
            return View(cliente);
        }

        public ActionResult Listado()
        {
            List<Cliente> listado = new List<Cliente>();

            return View();
        }

        // GET
        public ActionResult Actualizar(int codigo)
        {

            // Se busca un cliente
            Cliente cliente = new Cliente();

            return View(cliente);
        }

        public ActionResult Actualizar(int codigo, Cliente cliente)
        {
            return View(cliente);
        }

        public ActionResult Eliminar(int codigo)
        {
            return View();
        }

    }
}
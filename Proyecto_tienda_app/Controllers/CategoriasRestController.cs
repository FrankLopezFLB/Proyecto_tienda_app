using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Proyecto_tienda_app.DAO;

namespace Proyecto_tienda_app.Controllers
{
    public class CategoriasRestController : ApiController
    {
        categoriaDAO DAO = new categoriaDAO();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var categorias = DAO.listado();

            return Ok(categorias);
        }
    }
}

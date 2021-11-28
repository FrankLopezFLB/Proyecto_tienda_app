using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Proyecto_tienda_app.Models;
using Proyecto_tienda_app.DAO;

namespace Proyecto_tienda_app.Controllers
{
    public class ProductoController : Controller
    {
        //DAOS
        productosDAO productos = new productosDAO();
        categoriaDAO categorias = new categoriaDAO();
        // GET: Producto
        public ActionResult Index(int id = 0)
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
            //buscar
            Producto reg = productos.Buscar(id);
           if (reg == null) reg = new Producto();
            //Producto reg = (id == 0 ? new Producto() : productos.Buscar(id));
            //categorias
            ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.idcategoria);

            //productos
            ViewBag.productos = productos.listado();

            return View(reg);
        }
      [HttpPost]
        public ActionResult Index(string btncrud, Producto reg, HttpPostedFileBase archivo)

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
            switch (btncrud)

            {

                case "Agregar": return Agregar(reg,archivo);

                case "Editar": return Actualizar(reg,archivo);

                case "Eliminar": return Eliminar(reg);

                default: return RedirectToAction("Index", new { id = 0 });

            }

        }
       public ActionResult Agregar(Producto reg, HttpPostedFileBase archivo)

        {

            if (archivo == null || archivo.ContentLength <= 0)

            {

                ViewBag.mensaje = "Seleccione una imagen";
                ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.idcategoria);
                ViewBag.productos = productos.listado();

                return View(reg);

            }
            //si seleccionaste, lo guardamos en la carpeta FOTOS

            string ruta = "~/IMAGENES/" + System.IO.Path.GetFileName(archivo.FileName);

            archivo.SaveAs(Server.MapPath(ruta));



            //ejecucion del crud

            SqlParameter[] pars =

             {
       // new SqlParameter(){ParameterName="@codigo",Value=reg.codigo},

        new SqlParameter(){ParameterName="@nombre",Value=reg.nombre},

        new SqlParameter(){ParameterName="@descripcion",Value=reg.descripcion},

        new SqlParameter(){ParameterName="@idCat",Value=reg.idcategoria},

        new SqlParameter(){ParameterName="@stock",Value=reg.stock},

        new SqlParameter(){ParameterName="@precio",Value=reg.precio},

        new SqlParameter(){ParameterName="@estado",Value=1},

        new SqlParameter(){ParameterName="@imagen",Value=ruta}

        };



            ViewBag.mensaje = productos.CRUD("sp_insertProduct", pars, 1);

            ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.idcategoria);

            ViewBag.productos = productos.listado();

            return View(reg);

        }
        public ActionResult Actualizar(Producto reg, HttpPostedFileBase archivo)

        {
            if (archivo == null || archivo.ContentLength <= 0)

            {

                ViewBag.mensaje = "Seleccione una imagen";



                ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.idcategoria);

                ViewBag.productos = productos.listado();

                return View(reg);

            }
            //si seleccionaste, lo guardamos en la carpeta FOTOS

            string ruta = "~/IMAGENES/" + System.IO.Path.GetFileName(archivo.FileName);

            archivo.SaveAs(Server.MapPath(ruta));

            SqlParameter[] pars =

              {
                new SqlParameter(){ParameterName="@codigo",Value=reg.codigo},

                new SqlParameter(){ParameterName="@nombre",Value=reg.nombre},

                new SqlParameter(){ParameterName="@descripcion",Value=reg.descripcion},

                new SqlParameter(){ParameterName="@idCat",Value=reg.idcategoria},

                new SqlParameter(){ParameterName="@stock",Value=reg.stock},

                new SqlParameter(){ParameterName="@precio",Value=reg.precio},

                new SqlParameter(){ParameterName="@estado",Value=1},

                new SqlParameter(){ParameterName="@imagen",Value=ruta}

      };

            //ejecutar

            ViewBag.mensaje = productos.CRUD("sp_updateProduct", pars, 2);



            ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.codigo);

            ViewBag.productos = productos.listado();

            return View(reg);

        }


        public ActionResult Eliminar(Producto reg)

        {

            SqlParameter[] pars =

              {

        new SqlParameter(){ParameterName="@codigo",Value=reg.codigo}

        };

            //ejecutar

            ViewBag.mensaje = productos.CRUD("sp_deleteProduct", pars, 3);

            ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.codigo);

            ViewBag.productos = productos.listado();

            return View(reg);

        }
    } 
    
}
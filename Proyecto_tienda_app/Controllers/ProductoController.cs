﻿using System;
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
            //buscar
            Producto reg = productos.Buscar(id);
            if (reg == null) reg = new Producto();
            //categorias
            ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.codigo);
            //productos
            ViewBag.productos = productos.listado();
            return View(reg);
        }
        [HttpPost]
        public ActionResult Index(string btncrud, Producto reg)

        {

            switch (btncrud)

            {

                case "Create": return Agregar(reg,null);

                case "Edit": return Actualizar(reg,null);

                case "Delete": return Eliminar(reg);

                default: return RedirectToAction("Index", new { id = 0 });

            }

        }
        public ActionResult Agregar(Producto reg, HttpPostedFileBase archivo)

        {

            if (archivo == null || archivo.ContentLength <= 0)

            {

                ViewBag.mensaje = "Seleccione una imagen";



                ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.codigo);

                ViewBag.productos = productos.listado();

                return View(reg);

            }
            //si seleccionaste, lo guardamos en la carpeta FOTOS

            string ruta = "~/FOTOS/" + System.IO.Path.GetFileName(archivo.FileName);

            archivo.SaveAs(Server.MapPath(ruta));



            //ejecucion del crud

            SqlParameter[] pars =

             {

        new SqlParameter(){ParameterName="@nombre",Value=reg.nombre},

        new SqlParameter(){ParameterName="@descripcion",Value=reg.descripcion},

        new SqlParameter(){ParameterName="@idCat",Value=reg.idcategoria},

        new SqlParameter(){ParameterName="@stock",Value=reg.stock},

        new SqlParameter(){ParameterName="@precio",Value=reg.precio},

        new SqlParameter(){ParameterName="@imagen",Value=ruta}

        };



            ViewBag.mensaje = productos.CRUD("sp_insertProduct", pars, 1);



            ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.codigo);

            ViewBag.productos = productos.listado();

            return View(reg);

        }
        public ActionResult Actualizar(Producto reg, HttpPostedFileBase archivo)

        {
            if (archivo == null || archivo.ContentLength <= 0)

            {

                ViewBag.mensaje = "Seleccione una imagen";



                ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.codigo);

                ViewBag.productos = productos.listado();

                return View(reg);

            }
            //si seleccionaste, lo guardamos en la carpeta FOTOS

            string ruta = "~/FOTOS/" + System.IO.Path.GetFileName(archivo.FileName);

            archivo.SaveAs(Server.MapPath(ruta));

            SqlParameter[] pars =

              {
                new SqlParameter(){ParameterName="@codigo",Value=reg.codigo},

                new SqlParameter(){ParameterName="@nombre",Value=reg.nombre},

                new SqlParameter(){ParameterName="@descripcion",Value=reg.descripcion},

                new SqlParameter(){ParameterName="@idCat",Value=reg.idcategoria},

                new SqlParameter(){ParameterName="@stock",Value=reg.stock},

                new SqlParameter(){ParameterName="@precio",Value=reg.precio},

                new SqlParameter(){ParameterName="@imagen",Value=ruta}

      };

            //ejecutar

            ViewBag.mensaje = productos.CRUD("sp_updateProduct", pars, 1);



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

            ViewBag.mensaje = productos.CRUD("sp_deleteProduct", pars, 1);



            ViewBag.categorias = new SelectList(categorias.listado(), "codigo", "nombre", reg.codigo);

            ViewBag.productos = productos.listado();

            return View(reg);

        }
    } 
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Proyecto_tienda_app.Models;

namespace Proyecto_tienda_app.DAO
{
    public class productosDAO
    {
        
       public IEnumerable<Producto> listado()

        {
            
            List<Producto> temporal = new List<Producto>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)

            {

                SqlCommand cmd = new SqlCommand("exec sp_listProduct", cn.getcn);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())

                {

                    temporal.Add(new Producto()

                    { codigo = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        descripcion = dr.GetString(2),
                        idcategoria = dr.GetInt32(3),
                        stock = dr.GetInt32(4),
                        precio = dr.GetDecimal(5), 
                        estado = dr.GetInt32(6),
                        rutaimg=dr.GetString(7) });

                }

                dr.Close(); cn.getcn.Close();

            }

            return temporal;
        }
      /*  public IEnumerable<Producto> listado(string SP, SqlParameter[] pars = null)

        {
            conexionDAO cn = new conexionDAO();
            List<Producto> temporal = new List<Producto>();

            using (cn.getcn)

            {

                SqlCommand cmd = new SqlCommand(SP, cn.getcn);

                cmd.CommandType = CommandType.StoredProcedure;

                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())

                {

                    temporal.Add(new Producto()

                    {
                        codigo = (int)dr[0] ,
                        nombre = (string)dr[1],
                        descripcion = (string)dr[2],
                        idcategoria = (int)dr[3],
                        stock = (int)dr[4],
                        precio = (decimal)dr[5],
                        estado = (int)dr[6],
                        rutaimg = (string)dr[7]


                    });

                }

                dr.Close(); cn.getcn.Close();

            }

            return temporal;

        }*/


        public Producto Buscar(int id)
        {
            Producto reg = listado().Where(c => c.codigo == id).FirstOrDefault();

            return reg;

        }
        public string CRUD(string SP, SqlParameter[] pars = null, int op = 0)
        {
            conexionDAO cn = new conexionDAO();
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand(SP, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());
                cn.getcn.Open();
                int c = cmd.ExecuteNonQuery();// retorna la cantidad de afectados 
                if (op == 1) mensaje = c + "registro agregado";
                else if (op == 2) mensaje = c + "registro actualizado";
                else if (op == 3) mensaje = c + "registro eliminado";


            }
            catch (SqlException ex)
            {

                mensaje = ex.Message;
            }
            finally { cn.getcn.Close(); }
            return mensaje;
        }

    }
}
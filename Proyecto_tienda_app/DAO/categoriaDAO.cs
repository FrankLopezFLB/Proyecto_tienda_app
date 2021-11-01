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
    public class categoriaDAO
    {
        conexionDAO cn = new conexionDAO();

        public IEnumerable<Categoria> listado()

        {

            List<Categoria> temporal = new List<Categoria>();
            cn = new conexionDAO();
            using (cn.getcn)

            {

                SqlCommand cmd = new SqlCommand("exec sp_listCategoria", cn.getcn);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())

                {

                    temporal.Add(new Categoria()

                    { codigo = dr.GetInt32(0), nombre = dr.GetString(1) });

                }

                dr.Close(); cn.getcn.Close();

            }

            return temporal;
        }
    
}
}
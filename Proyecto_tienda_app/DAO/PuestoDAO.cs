using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Proyecto_tienda_app.Models;
using System.Data.SqlClient;


namespace Proyecto_tienda_app.DAO
{
    public class PuestoDAO
    {
        public IEnumerable<Puesto> Listado()
        {
            List<Puesto> temporal = new List<Puesto>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("sp_alfredo_puestos", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Puesto()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1)
                    });
                }

                dr.Close();
                cn.getcn.Close();
            }

            return temporal;
        }

    }
}
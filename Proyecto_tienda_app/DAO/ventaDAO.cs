using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Proyecto_tienda_app.Models;
using System.Data.SqlClient;

namespace Proyecto_tienda_app.DAO
{
    public class ventaDAO
    {
        public Response RegistrarCompras(Boleta boleta, List<Item> carrito)
        {
            Response respuesta = new Response();

            conexionDAO cn = new conexionDAO();
            cn.getcn.Open();

            SqlTransaction tr = cn.getcn.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                SqlCommand cm = new SqlCommand("sp_generar_boleta", cn.getcn, tr);
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("@precioTotal", SqlDbType.Decimal).Value = boleta.PrecioTotal;
                cm.Parameters.Add("@codigoCliente", SqlDbType.Int).Value = boleta.CodigoCliente;
                cm.Parameters.Add("@n", SqlDbType.Int).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();

                int codBol = (int)cm.Parameters["@n"].Value;

                foreach (Item it in carrito)
                {
                    cm = new SqlCommand("sp_agregar_detalle_boleta", cn.getcn, tr);
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("@codigoBol", SqlDbType.Int).Value = codBol;
                    cm.Parameters.Add("@codigoProd", SqlDbType.Int).Value = it.codigo;
                    cm.Parameters.Add("@importe", SqlDbType.Decimal).Value = it.monto;
                    cm.Parameters.Add("@cantidad", SqlDbType.Int).Value = it.cantidad;
                    cm.ExecuteNonQuery();

                }

                tr.Commit();
                respuesta.HayError = false;
                respuesta.Mensaje = $"Se ha registrado la venta con boleta numero {codBol}";

            }
            catch (SqlException ex)
            {
                respuesta.HayError = true;
                respuesta.Mensaje = ex.Message;

            } finally
            {
                cn.getcn.Close();
            }

            return respuesta;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Proyecto_tienda_app.Models;
using System.Data.SqlClient;

namespace Proyecto_tienda_app.DAO
{
    public class boletaDAO
    {
        public IEnumerable<Boleta> ListadoPorCliente(int codCli)
        {
            List<Boleta> temporal = new List<Boleta>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("sp_lista_boletas_cliente", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codCli", codCli);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Boleta()
                    {
                        Codigo = dr.GetInt32(0),
                        PrecioTotal = dr.GetDecimal(1),
                        Fecha = dr.GetDateTime(2),
                        CodigoCliente = dr.GetInt32(3)
                    });
                }

                dr.Close();
                cn.getcn.Close();
            }

            return temporal;
        }

        public IEnumerable<DetalleBoletaProducto> DetalleProductoInfo(int codigoBol)
        {
            List<DetalleBoletaProducto> temporal = new List<DetalleBoletaProducto>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("sp_detalle_bol_producto", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigoBol", codigoBol);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new DetalleBoletaProducto()
                    {
                        CodigoBoleta = dr.GetInt32(0),
                        CodigoProducto = dr.GetInt32(1),
                        NombreProducto = dr.GetString(2),
                        RutaImgProducto = dr.GetString(3),
                        Cantidad = dr.GetInt32(4),
                        Importe = dr.GetDecimal(5)
                    });
                }

                dr.Close();
                cn.getcn.Close();
            }

            return temporal;
        }
    }
}
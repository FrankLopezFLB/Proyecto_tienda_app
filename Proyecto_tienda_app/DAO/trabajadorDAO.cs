using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Proyecto_tienda_app.Models;
using System.Data.SqlClient;

namespace Proyecto_tienda_app.DAO
{
    public class trabajadorDAO
    {
        public IEnumerable<Trabajador> Listado()
        {
            List<Trabajador> temporal = new List<Trabajador>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Trabajador()
                    {
                        Codigo = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Apellido = dr.GetString(2),
                        Telefono = dr.GetString(3),
                        Direccion = dr.GetString(4),
                        Email = dr.GetString(5),
                        Clave = dr.GetString(6),
                        Estado = dr.GetInt32(7)
                    });
                }

                dr.Close();
                cn.getcn.Close();
            }

            return temporal;
        }

        public Trabajador Buscar(int id)
        {
            return Listado().FirstOrDefault(p => p.Codigo == id);
        }

        public Response Registrar(Trabajador trabajador)
        {
            conexionDAO cn = new conexionDAO();
            Response response = new Response();

            try
            {
                SqlCommand command = new SqlCommand("sp_registrar_trabajador", cn.getcn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@nombre", trabajador.Nombre);
                command.Parameters.AddWithValue("@apellido", trabajador.Apellido);
                command.Parameters.AddWithValue("@telefono", trabajador.Telefono);
                command.Parameters.AddWithValue("@direccion", trabajador.Direccion);
                command.Parameters.AddWithValue("@email", trabajador.Email);
                command.Parameters.AddWithValue("@clave", trabajador.Clave);

                cn.getcn.Open();

                response.FilasAfectadas = command.ExecuteNonQuery();
                response.Mensaje = "Trabajador registrado correctamente";
                response.HayError = false;

            }
            catch (Exception e)
            {
                response.HayError = true;
                response.Mensaje = e.Message;

            }
            finally
            {
                cn.getcn.Close();
            }

            return response;
        }

        public Response Actualizar(Trabajador trabajador)
        {
            conexionDAO cn = new conexionDAO();
            Response response = new Response();

            try
            {
                SqlCommand command = new SqlCommand("sp_actualizar_trabajador", cn.getcn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@codigo", trabajador.Codigo);
                command.Parameters.AddWithValue("@nombre", trabajador.Nombre);
                command.Parameters.AddWithValue("@apellido", trabajador.Apellido);
                command.Parameters.AddWithValue("@telefono", trabajador.Telefono);
                command.Parameters.AddWithValue("@direccion", trabajador.Direccion);
                command.Parameters.AddWithValue("@email", trabajador.Email);
                command.Parameters.AddWithValue("@clave", trabajador.Clave);

                cn.getcn.Open();

                response.FilasAfectadas = command.ExecuteNonQuery();
                response.Mensaje = "Trabajador actualizado correctamente";
                response.HayError = false;

            }
            catch (Exception e)
            {
                response.HayError = true;
                response.Mensaje = e.Message;

            }
            finally
            {
                cn.getcn.Close();
            }

            return response;
        }

        public Response Eliminar(int codigo)
        {
            conexionDAO cn = new conexionDAO();
            Response response = new Response();

            try
            {
                SqlCommand command = new SqlCommand("sp_eliminar_trabajador", cn.getcn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@codigo", codigo);

                cn.getcn.Open();

                response.FilasAfectadas = command.ExecuteNonQuery();
                response.Mensaje = "Trabajador eliminado correctamente";
                response.HayError = false;
            }
            catch (Exception e)
            {
                response.Mensaje = e.Message;
                response.HayError = false;

            }
            finally
            {
                cn.getcn.Close();
            }

            return response;
        }

        public Response CRUD(string procedure, SqlParameter[] sqlParameters = null, int opcion = 0)
        {
            conexionDAO cn = new conexionDAO();
            Response response = new Response();

            try
            {
                SqlCommand cmd = new SqlCommand(procedure, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (sqlParameters != null)
                {
                    cmd.Parameters.AddRange(sqlParameters.ToArray());
                }

                cn.getcn.Open();

                response.FilasAfectadas = cmd.ExecuteNonQuery();

                switch (opcion)
                {
                    case 1:
                        response.Mensaje = "Trabajador registrado correctamente";
                        break;
                    case 2:
                        response.Mensaje = "Trabajador actualizado correctamente";
                        break;
                    case 3:
                        response.Mensaje = "Trabajador eliminado correctamente";
                        break;
                }

                response.HayError = false;

            }
            catch (SqlException ex)
            {
                response.HayError = false;
                response.Mensaje = ex.Message;
            }
            finally
            {
                cn.getcn.Close();
            }

            return response;
        }

    }
}
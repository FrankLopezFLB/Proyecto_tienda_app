using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Proyecto_tienda_app.Models;
using System.Data.SqlClient;

namespace Proyecto_tienda_app.DAO
{
    public class usuarioDAO
    {
        private static usuarioDAO instancia = null;

        public usuarioDAO()
        {

        }
        public static usuarioDAO Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new usuarioDAO();
                }

                return instancia;
            }
        }
        public IEnumerable<Usuario> Listado()
        {
            List<Usuario> temporal = new List<Usuario>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Usuario()
                    {
                        Codigo = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Apellido = dr.GetString(2),
                        Telefono = dr.GetString(3),
                        Direccion = dr.GetString(4),
                        Email = dr.GetString(5),
                        Clave = dr.GetString(6),
                        Dni = dr.GetString(7),
                        Estado = dr.GetInt32(8),
                        puestoID = dr.GetInt32(9)
                    });
                }

                dr.Close();
                cn.getcn.Close();
            }

            return temporal;
        }
        
        public Usuario Buscar(int id)
        {
            return Listado().FirstOrDefault(p => p.Codigo == id);
        }

        public Response Registrar(Usuario usuario)
        {
            conexionDAO cn = new conexionDAO();
            Response response = new Response();

            try
            {
                SqlCommand command = new SqlCommand("sp_createUser", cn.getcn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@telefono", usuario.Telefono);
                command.Parameters.AddWithValue("@direccion", usuario.Direccion);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@clave", usuario.Clave);
                command.Parameters.AddWithValue("@dni", usuario.Dni);
                cn.getcn.Open();

                response.FilasAfectadas = command.ExecuteNonQuery();
                response.Mensaje = "Usuario registrado correctamente";
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
        public Usuario Login(string Email, string Clave)
        {
            conexionDAO cn = new conexionDAO();
            Usuario usuario = null;

            try
            {
                SqlCommand command = new SqlCommand("sp_login", cn.getcn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@email", Email);
                command.Parameters.AddWithValue("@clave", Clave);
                cn.getcn.Open();

                SqlDataReader dr = command.ExecuteReader();

                if(dr.Read())
                {
                    usuario = new Usuario()
                    {
                        Codigo = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Apellido = dr.GetString(2),
                        Telefono = dr.GetString(3),
                        Direccion = dr.GetString(4),
                        Email = dr.GetString(5),
                        Clave = dr.GetString(6),
                        Dni = dr.GetString(7),
                        Estado = dr.GetInt32(8),
                        puestoID = dr.GetInt32(9)
                    };
                }
                dr.Close();
                cn.getcn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return usuario;
        }

        public Response Actualizar(Usuario usuario)
        {
            conexionDAO cn = new conexionDAO();
            Response response = new Response();

            try
            {
                SqlCommand command = new SqlCommand("sp_actualizar_trabajador", cn.getcn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@codigo", usuario.Codigo);
                command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@telefono", usuario.Telefono);
                command.Parameters.AddWithValue("@direccion", usuario.Direccion);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@clave", usuario.Clave);
                command.Parameters.AddWithValue("@dni", usuario.Dni);

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
                        response.Mensaje = "Usuario registrado correctamente";
                        break;
                    case 2:
                        response.Mensaje = "Usuario actualizado correctamente";
                        break;
                    case 3:
                        response.Mensaje = "Usuario eliminado correctamente";
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
using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Datos
{
    public class DGrupo
    {
        public static List<string> LlenarCmbGrupos()
        {
            DataTable tabla = new DataTable();
            List<string> nombres = new List<string>();
            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select nombre from grupo where estatus = 1", sqlCon);
                sqlDa.Fill(tabla);

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    nombres.Add(Convert.ToString(tabla.Rows[i]["nombre"]));
                    System.Diagnostics.Debug.WriteLine(nombres[i]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            { // Este codigo se va a ejecutar aunque haya alguna excepcion. **SIEMPRE SE CERRARÁ LA CONEXIÓN**

                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return nombres;
        }

        public static DataTable ListarGrupos()
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select g.idGrupo, g.claveGrupo, g.nombre, c.nombre as 'nombreCarrera'" +
                    " from Grupo g inner join Carrera c on (g.idCarrera = c.idCarrera) where g.estatus = 1", sqlCon);
                sqlDa.Fill(tabla);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            { // Este codigo se va a ejecutar aunque haya alguna excepcion. **SIEMPRE SE CERRARÁ LA CONEXIÓN**

                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return tabla;
        }

        public static List<string> LlenarCmbCarreras()
        {
            DataTable tabla = new DataTable();
            List<string> nombres = new List<string>();
            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select nombre from Carrera where estatus = 1", sqlCon);
                sqlDa.Fill(tabla);

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    nombres.Add(Convert.ToString(tabla.Rows[i]["nombre"]));
                    System.Diagnostics.Debug.WriteLine(nombres[i]);                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            { // Este codigo se va a ejecutar aunque haya alguna excepcion. **SIEMPRE SE CERRARÁ LA CONEXIÓN**

                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return nombres;
        }
        public static int ObtenerIdGrupoPNombre(string nombreGrupo)
        {
            int _idGrupo;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_obtenerIdGrupo", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = nombreGrupo;

                //Agregamos los parametros de salida (idgrupo)
                SqlParameter var_idGrupo = new SqlParameter();
                var_idGrupo.ParameterName = "@var_idGrupo";
                var_idGrupo.SqlDbType = SqlDbType.Int;
                var_idGrupo.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(var_idGrupo);

                sqlConnection.Open();
                _idGrupo = (int)command.ExecuteScalar();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;

            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return _idGrupo;
        }

        public static int ObtenerIdCarreraPNombre(string nombreCarrera)
        {
            int _idCarrera;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_obtenerIdCarrera", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = nombreCarrera;
                
                //Agregamos los parametros de salida (idgrupo)
                SqlParameter idCarrera = new SqlParameter();
                idCarrera.ParameterName = "@var_idCarrera";
                idCarrera.SqlDbType = SqlDbType.Int;
                idCarrera.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(idCarrera);

                sqlConnection.Open();
                _idCarrera = (int)command.ExecuteScalar();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
                
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return _idCarrera;
        }

        public static string BajaGrupo(int id)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarGrupoEstatus", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idGrupo", SqlDbType.Int).Value = id;

                //Agregamos los parametros de salida (claveCarrera)
                SqlParameter var_estatus = new SqlParameter();
                var_estatus.ParameterName = "@var_estatus";
                var_estatus.SqlDbType = SqlDbType.Int;
                var_estatus.Direction = ParameterDirection.Output;

                command.Parameters.Add(var_estatus);

                //Abrimos la conexion y guardamos el resultado en respuesta

                sqlConnection.Open();

                if (command.ExecuteNonQuery() == 1) // el 1 respresenta un resultado exitoso
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = "Se eliminó el grupo";
                }
                else
                {
                    respuesta = "No se pudo completar la solicitud...";
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }

            return respuesta;
        }

        public static DataTable ObtenerGrupo(int n)
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM GRUPO WHERE idGrupo = @_idGrupo", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@_idGrupo", n);
                sqlDa.Fill(tabla);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            { // Este codigo se va a ejecutar aunque haya alguna excepcion. **SIEMPRE SE CERRARÁ LA CONEXIÓN**

                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return tabla;
        }

        public static DataTable ObtenerGrupoPorId(int n)
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM GRUPO WHERE idGrupo = @_idGrupo", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@_idGrupo", n);
                sqlDa.Fill(tabla);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            { // Este codigo se va a ejecutar aunque haya alguna excepcion. **SIEMPRE SE CERRARÁ LA CONEXIÓN**

                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return tabla;
        }
        public static string InsertarGrupo(Grupo grupo)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_insertarGrupo", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = grupo.Nombre;
                command.Parameters.Add("@var_idCarrera", SqlDbType.VarChar).Value = grupo.IdCarrera;

                //Agregamos los parametros de salida (idgrupo)
                SqlParameter idGrupo = new SqlParameter();
                idGrupo.ParameterName = "@var_idGrupo";
                idGrupo.SqlDbType = SqlDbType.Int;
                idGrupo.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (claveCarrera)
                SqlParameter claveGrupo = new SqlParameter();
                claveGrupo.ParameterName = "@var_claveGrupo";
                claveGrupo.SqlDbType = SqlDbType.VarChar;
                claveGrupo.Size = 30;
                claveGrupo.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(idGrupo);
                command.Parameters.Add(claveGrupo);

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 respresenta un resultado exitoso (1 row affected)
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = grupo.Nombre + " insertado correctamente. " +
                        "\nClave generada: " + Convert.ToString(claveGrupo);
                }
                else
                {
                    respuesta = "No se pudo completar la solicitud...";
                }


            }
            catch (Exception e)
            {
                respuesta = null;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return respuesta;
        }

        public static string AcutalizarGrupo(Grupo grupo)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarGrupo", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idGrupo", SqlDbType.Int).Value = grupo.IdGrupo;
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = grupo.Nombre;
                command.Parameters.Add("@var_estatus", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@var_idCarrera", SqlDbType.Int).Value = grupo.IdCarrera;

                //Agregamos los parametros de salida (claveCarrera)
                SqlParameter var_salidaConfirmacion = new SqlParameter();
                var_salidaConfirmacion.ParameterName = "@var_salidaConfirmacion";
                var_salidaConfirmacion.SqlDbType = SqlDbType.Int;
                var_salidaConfirmacion.Direction = ParameterDirection.Output;

                command.Parameters.Add(var_salidaConfirmacion);

                //Abrimos la conexion y guardamos el resultado en respuesta

                sqlConnection.Open();

                if (command.ExecuteNonQuery() == 1) // el 1 respresenta un resultado exitoso
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = "Se guardaron los cambios en" + grupo.Nombre;
                }
                else
                {
                    respuesta = "No se pudo completar la solicitud...";
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }

            return respuesta;
        }
    }
}
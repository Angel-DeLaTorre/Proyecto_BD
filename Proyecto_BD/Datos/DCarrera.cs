﻿using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Datos 
{
    public class DCarrera
    {
        public static DataTable ListarCarreras()
        {
            SqlDataReader resultado; // lee una secuencia de filas en la tabla
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlCommand comando = new SqlCommand("sp_listarCarreras", sqlCon); // este es el comando que se va a ejecutar el la base de datos
                comando.CommandType = CommandType.StoredProcedure;
                sqlCon.Open();
                //Se ejecuta el comando
                resultado = comando.ExecuteReader();
                //se carga en el objeto tabla
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            { // Este codigo se va a ejecutar aunque haya alguna excepcion. **SIEMPRE SE CERRARÁ LA CONEXIÓN**

                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }
        public static string InsertarCarrera(Carrera carrera)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_insertarCarrera", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = carrera.Nombre;

                //Agregamos los parametros de salida (idCarrera)
                SqlParameter idCarrera = new SqlParameter();
                idCarrera.ParameterName = "@var_idCarrera";
                idCarrera.SqlDbType = SqlDbType.Int;
                idCarrera.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (claveCarrera)
                SqlParameter claveCarrera = new SqlParameter();
                claveCarrera.ParameterName = "@var_claveCarrera";
                claveCarrera.SqlDbType = SqlDbType.VarChar;
                claveCarrera.Size = 30;
                claveCarrera.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(idCarrera);
                command.Parameters.Add(claveCarrera);

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 respresenta un resultado exitoso (1 row affected)
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = carrera.Nombre + " insertada correctamente. " +
                        "\nClave generada: " + Convert.ToString(claveCarrera);
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
        public static DataTable ObtenerCarreraPorId(int n)
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM CARRERA WHERE idCarrera = @_idCarrera", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@_idCarrera", n);
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
                _idCarrera = command.ExecuteNonQuery();
                _idCarrera = Convert.ToInt32(command.Parameters["@var_idCarrera"].Value);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                throw e;

            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return _idCarrera;
        }
        public static string AcutalizarCarrera(Carrera carrera)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarCarrera", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idCarrera", SqlDbType.Int).Value = carrera.IdCarrera;
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = carrera.Nombre;
                command.Parameters.Add("@var_estatus", SqlDbType.Int).Value = 1;

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
                    respuesta = "Se guardaron los cambios en" + carrera.Nombre;
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
        public static string BajaCarrera(int id)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarCarreraEstatus", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idCarrera", SqlDbType.Int).Value = id;

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
                    respuesta = "Se eliminó la carerra";
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

        public static List<Carrera> listCarrera()
        {
            List<Carrera> list = new List<Carrera>();
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                int count = 0;
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select idCarrera,nombre from carrera WHERE estatus = 1";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows == false)
                        {
                            list = null;
                            return list;
                        }
                        while (reader.Read())
                        {
                            Carrera c = new Carrera();
                            c.IdCarrera = reader.GetInt32(reader.GetOrdinal("idCarrera"));
                            c.Nombre = reader.GetString(reader.GetOrdinal("nombre"));
                            list.Add(c);
                        }
                    }
                }
                return list;
            }
            catch (Exception exc)
            {
                return list;
            }

        }
    }
}
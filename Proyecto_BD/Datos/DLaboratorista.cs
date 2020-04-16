using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Datos
{
    public class DLaboratorista
    {
        public static DataTable listarAltaLaboratoristas()
        {
            SqlDataReader resultado; // lee una secuencia de filas en la tabla
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlCommand comando = new SqlCommand("sp_ListarLaboratoristasAlta", sqlCon); // este es el comando que se va a ejecutar el la base de datos
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

        public static DataTable listarBajaLaboratoristas()
        {
            SqlDataReader resultado; // lee una secuencia de filas en la tabla
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlCommand comando = new SqlCommand("sp_ListarLaboratoristasBaja", sqlCon); // este es el comando que se va a ejecutar el la base de datos
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

        public static List<Laboratorio> listarLaboratoriosDisponibles()
        {
            List<Laboratorio> list = new List<Laboratorio>();
            
            try
            {
                SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
                int count = 0;
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"SELECT LA.idLaboratorio, LA.nombre FROM Laboratorista L right JOIN Laboratorio LA 
                                            ON L.idLaboratorio = LA.idLaboratorio WHERE LA.estatus = 1 GROUP BY LA.idLaboratorio, LA.nombre 
                                            HAVING COUNT(*)<2";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows == false)
                        {
                            list = null;
                            return list;
                        }
                        while (reader.Read())
                        {
                            Laboratorio l = new Laboratorio();
                            l.IdLaboratorio = reader.GetInt32(reader.GetOrdinal("idLaboratorio"));
                            l.Nombre = reader.GetString(reader.GetOrdinal("nombre"));
                            list.Add(l);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                return list;
            }
        }

        public static int insertarLaboratorista(Laboratorista l)
        {
            int respuesta = 0;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_insertarLaboratorista", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = l.Persona.nombre;
                command.Parameters.Add("@var_apPaterno", SqlDbType.VarChar).Value = l.Persona.apPaterno;
                command.Parameters.Add("@var_apMaterno", SqlDbType.VarChar).Value = l.Persona.apMaterno;
                command.Parameters.Add("@var_direccion", SqlDbType.VarChar).Value = l.Persona.direccion;
                command.Parameters.Add("@var_codigoPostal", SqlDbType.VarChar).Value = l.Persona.codigoPostal;
                command.Parameters.Add("@var_telefono", SqlDbType.VarChar).Value = l.Persona.telefono;
                command.Parameters.Add("@var_sexo", SqlDbType.Char).Value = l.Persona.sexo;
                command.Parameters.Add("@var_rol", SqlDbType.Int).Value = l.Usuario.rol;
                command.Parameters.Add("@var_turno", SqlDbType.VarChar).Value = l.turno;
                command.Parameters.Add("@var_idLaboratorio", SqlDbType.Int).Value = l.Laboratorio.IdLaboratorio;

                //Agregamos los parametros de salida (idPersona)
                SqlParameter idPersona = new SqlParameter();
                idPersona.ParameterName = "@var_idPersona";
                idPersona.SqlDbType = SqlDbType.Int;
                idPersona.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (idUsuario)
                SqlParameter idUsuario = new SqlParameter();
                idUsuario.ParameterName = "@var_idUsuario";
                idUsuario.SqlDbType = SqlDbType.Int;
                idUsuario.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (idLaboratorista)
                SqlParameter idLaboratorista = new SqlParameter();
                idLaboratorista.ParameterName = "@var_idLaboratorista";
                idLaboratorista.SqlDbType = SqlDbType.Int;
                idLaboratorista.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (claveLaboratorista)
                SqlParameter claveLaboratorista = new SqlParameter();
                claveLaboratorista.ParameterName = "@var_claveLaboratorista";
                claveLaboratorista.SqlDbType = SqlDbType.VarChar;
                claveLaboratorista.Size = 30;
                claveLaboratorista.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(idPersona);
                command.Parameters.Add(idUsuario);
                command.Parameters.Add(idLaboratorista);
                command.Parameters.Add(claveLaboratorista);

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 representa un resultado exitoso (1 row affected)
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = 1;
                }
                else
                {
                    respuesta = 0;
                }


            }
            catch (Exception e)
            {
                respuesta = 0;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return respuesta;
        }

        public static DataTable obtenerLaboratorista(int n)
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select L.idLaboratorista, L.claveLaboratorista, P.nombre, P.apMaterno, P.apPaterno, " +
                    "P.direccion, P.codigoPostal, P.telefono, P.sexo, U.contrasenia, U.rol, L.turno, LA.idLaboratorio " +
                    "FROM Laboratorista L INNER JOIN Persona P " +
                    "ON L.idPersona = P.idPersona INNER JOIN Laboratorio LA " +
                    "ON L.idLaboratorio = LA.idLaboratorio INNER JOIN Usuario U " +
                    "ON L.idUsuario = U.idUsuario where L.idLaboratorista=@_idLaboratorista", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@_idLaboratorista", n);
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

        public static int acutalizarLaboratorista(Laboratorista l)
        {
            int respuesta = 0;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarLaboratorista", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = l.Persona.nombre;
                command.Parameters.Add("@var_apPaterno", SqlDbType.VarChar).Value = l.Persona.apPaterno;
                command.Parameters.Add("@var_apMaterno", SqlDbType.VarChar).Value = l.Persona.apMaterno;
                command.Parameters.Add("@var_direccion", SqlDbType.VarChar).Value = l.Persona.direccion;
                command.Parameters.Add("@var_codigoPostal", SqlDbType.VarChar).Value = l.Persona.codigoPostal;
                command.Parameters.Add("@var_telefono", SqlDbType.VarChar).Value = l.Persona.telefono;
                command.Parameters.Add("@var_sexo", SqlDbType.Char).Value = l.Persona.sexo;
                command.Parameters.Add("@var_contrasenia", SqlDbType.VarChar).Value = l.Usuario.contrasenia;
                command.Parameters.Add("@var_rol", SqlDbType.Int).Value = l.Usuario.rol;
                command.Parameters.Add("@var_idLaboratorista", SqlDbType.Int).Value = l.idLaboratorista;
                command.Parameters.Add("@var_turno", SqlDbType.VarChar).Value = l.turno;
                command.Parameters.Add("@var_estatus", SqlDbType.Int).Value = l.estatus;
                command.Parameters.Add("@var_idLaboratorio", SqlDbType.Int).Value = l.Laboratorio.IdLaboratorio;

                //Agregamos los parametros de salida (claveCarrera)
                SqlParameter var_salidaConfirmacion = new SqlParameter();
                var_salidaConfirmacion.ParameterName = "@var_salidaConfirmacion";
                var_salidaConfirmacion.SqlDbType = SqlDbType.Int;
                var_salidaConfirmacion.Direction = ParameterDirection.Output;

                command.Parameters.Add(var_salidaConfirmacion);

                //Abrimos la conexion y guardamos el resultado en respuesta

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 respresenta un resultado exitoso
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = 1;
                }
                else
                {
                    respuesta = 0;
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

        public static string bajaLaboratorista(int id)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarLaboratoristaEstatus", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idLaboratorista", SqlDbType.Int).Value = id;

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
                    respuesta = "Se eliminó el laboratorio";
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Proyecto_BD.Models;

namespace Proyecto_BD.Datos
{
    public class DAlumno
    {
        public static DataTable ListarAlumnos()
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                //Esto se mostrara en la tabla del index se redujeron los campos porque despues se mostrara todo en la vista detalle
                SqlDataAdapter sqlDa = new SqlDataAdapter("select a.idAlumno, a.matricula, CONCAT(p.apPaterno, ' ', p.apMaterno, ' ', p.nombre) as nombreCompleto, " +
                    "p.direccion, c.nombre as carrera, g.nombre as grupo " +
                    "from Alumno a inner join Carrera c on (a.idCarrera = c.idCarrera) " +
                    "inner join Grupo g on (a.idGrupo = g.idGrupo) " +
                    "inner join Persona p on (a.idPersona = p.idPersona) where a.estatus = 1", sqlCon);

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

        public static DataTable ObtenerAlumno(int n)
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select p.nombre as nombrePersona, p.apPaterno, p.apMaterno, p.direccion, p.codigoPostal, " +
                    "p.telefono, p.sexo, a.idCarrera, a.idGrupo, a.idAlumno, u.contrasenia from Alumno a inner join Persona p on (a.idPersona = p.idPersona) " +
                    "inner join Usuario u on (a.idUsuario = u.idUsuario)" +
                    "where a.idAlumno = @_idAlumno", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@_idAlumno", n);
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
        public static string InsertarAlumno(Alumno alumno)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_insertarAlumno", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros: (Datos persona)
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = alumno.Persona.nombre;
                command.Parameters.Add("@var_apPaterno", SqlDbType.VarChar).Value = alumno.Persona.apPaterno;
                command.Parameters.Add("@var_apMaterno", SqlDbType.VarChar).Value = alumno.Persona.apMaterno;
                command.Parameters.Add("@var_direccion", SqlDbType.VarChar).Value = alumno.Persona.direccion;
                command.Parameters.Add("@var_codigoPostal", SqlDbType.VarChar).Value = Convert.ToString(alumno.Persona.codigoPostal);
                command.Parameters.Add("@var_telefono", SqlDbType.VarChar).Value = Convert.ToString(alumno.Persona.telefono);
                command.Parameters.Add("@var_sexo", SqlDbType.VarChar).Value = Convert.ToString(alumno.Persona.sexo);

                //Agregamos los datos del alumno
                command.Parameters.Add("@var_idCarrera", SqlDbType.Int).Value = alumno.idCarrera;
                command.Parameters.Add("@var_idGrupo", SqlDbType.Int).Value = alumno.idGrupo;


                //Agregamos los parametros de salida (idPersona)
                SqlParameter var_idPersona = new SqlParameter();
                var_idPersona.ParameterName = "@var_idPersona";
                var_idPersona.SqlDbType = SqlDbType.Int;
                var_idPersona.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (idUsuario)
                SqlParameter var_idUsuario = new SqlParameter();
                var_idUsuario.ParameterName = "@var_idUsuario";
                var_idUsuario.SqlDbType = SqlDbType.Int;
                var_idUsuario.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (idAlumno)
                SqlParameter var_idAlumno = new SqlParameter();
                var_idAlumno.ParameterName = "@var_idAlumno";
                var_idAlumno.SqlDbType = SqlDbType.Int;
                var_idAlumno.Direction = ParameterDirection.Output;


                //Agregamos los parametros de salida (matricula)
                SqlParameter var_matricula = new SqlParameter();
                var_matricula.ParameterName = "@var_matricula";
                var_matricula.SqlDbType = SqlDbType.VarChar;
                var_matricula.Size = 10;
                var_matricula.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(var_idPersona);
                command.Parameters.Add(var_idUsuario);
                command.Parameters.Add(var_idAlumno);
                command.Parameters.Add(var_matricula);

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 respresenta un resultado exitoso (1 row affected)
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = alumno.Persona.nombre + " insertado correctamente. " +
                        "\nMatrícula generada: " + Convert.ToString(var_matricula);
                }
                else
                {
                    respuesta = "No se pudo completar la solicitud...";
                }



            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Source);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return respuesta;
        }

        public static string AcutalizarAlumno(Alumno alumno, string pwd)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarAlumno", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros: (Datos persona)
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = alumno.Persona.nombre;
                command.Parameters.Add("@var_apPaterno", SqlDbType.VarChar).Value = alumno.Persona.apPaterno;
                command.Parameters.Add("@var_apMaterno", SqlDbType.VarChar).Value = alumno.Persona.apMaterno;
                command.Parameters.Add("@var_direccion", SqlDbType.VarChar).Value = alumno.Persona.direccion;
                command.Parameters.Add("@var_codigoPostal", SqlDbType.VarChar).Value = Convert.ToString(alumno.Persona.codigoPostal);
                command.Parameters.Add("@var_telefono", SqlDbType.VarChar).Value = Convert.ToString(alumno.Persona.telefono);
                command.Parameters.Add("@var_sexo", SqlDbType.VarChar).Value = Convert.ToString(alumno.Persona.sexo);

                //Datos de usuario
                command.Parameters.Add("@var_contrasenia", SqlDbType.VarChar).Value = pwd;

                //Datos alumno
                command.Parameters.Add("@var_idAlumno", SqlDbType.Int).Value = alumno.idAlumno;
                command.Parameters.Add("@var_estatus", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@var_idCarrera", SqlDbType.Int).Value = alumno.idCarrera;
                command.Parameters.Add("@var_idGrupo", SqlDbType.Int).Value = alumno.idGrupo;


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
                    respuesta = "Se guardaron los cambios en" + alumno.Persona.nombre;
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

        public static string BajaAlumno(int id)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarAlumnoEstatus", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idAlumno", SqlDbType.Int).Value = id;

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
    }
}
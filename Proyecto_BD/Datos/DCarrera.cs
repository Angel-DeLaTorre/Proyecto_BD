using Proyecto_BD.Models;
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
                throw e;
                respuesta = e.ToString();
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return respuesta;
        }
    }
}
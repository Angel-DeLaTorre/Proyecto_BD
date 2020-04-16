using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Proyecto_BD.Models;

namespace Proyecto_BD.Datos 
{
    public class DMaterial
    {

        public static DataTable listarMateriales()
        {
            SqlDataReader resultado; // lee una secuencia de filas en la tabla
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlCommand comando = new SqlCommand("sp_listarMaterial", sqlCon); // este es el comando que se va a ejecutar el la base de datos
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



        public static int insertarMaterial(Material m)
        {
            int respuesta = -1;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_insertarMaterial", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = m.Nombre;
                command.Parameters.Add("@var_descripcion", SqlDbType.VarChar).Value = m.Descripcion;
                command.Parameters.Add("@var_costoDevolucion", SqlDbType.Float).Value = m.CostoDevolucion;
                command.Parameters.Add("@var_fotografia", SqlDbType.VarChar).Value = m.Fotografia;

                //Agregamos los parametros de salida (idCarrera)
                SqlParameter idMat = new SqlParameter();
                idMat.ParameterName = "@var_idMaterial";
                idMat.SqlDbType = SqlDbType.Int;
                idMat.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (claveCarrera)
                SqlParameter claveMat = new SqlParameter();
                claveMat.ParameterName = "@var_claveMaterial";
                claveMat.SqlDbType = SqlDbType.VarChar;
                claveMat.Size = 30;
                claveMat.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(idMat);
                command.Parameters.Add(claveMat);

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 respresenta un resultado exitoso (1 row affected)
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = 1;
                }
                else
                {
                    respuesta = 2;
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


        public static DataTable obtenerMaterial(int n)
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Material WHERE idMaterial = @_idMaterial", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@_idMaterial", n);
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


        public static int acutalizarMaterial(Material m)
        {
            int respuesta = -1;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarMaterial", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idMaterial", SqlDbType.Int).Value = m.IdMaterial;
                command.Parameters.Add("@var_nombre", SqlDbType.VarChar).Value = m.Nombre;
                command.Parameters.Add("@var_descripcion", SqlDbType.VarChar).Value = m.Descripcion;
                command.Parameters.Add("@var_costoDevolucion", SqlDbType.Float).Value = m.CostoDevolucion;
                command.Parameters.Add("@var_fotografia", SqlDbType.VarChar).Value = m.Fotografia;


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
                    respuesta = 1;
                }
                else
                {
                    respuesta = 2;
                }


            }
            catch (Exception e)
            {
               respuesta = 2;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }

            return respuesta;
        }
    }
}
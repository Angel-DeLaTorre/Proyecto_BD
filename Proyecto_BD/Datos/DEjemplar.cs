using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Datos
{
    public class DEjemplar
    {
        public static DataTable ListarEjemplares()
        {
            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select e.idEjemplar, e.claveEjemplar, e.fechaCompra, e.prestado, l.nombre as 'Nombre Lab', " +
                    "m.nombre as 'Nombre Material' " +
                    "from Ejemplar e  inner join Material m on (e.idMaterial = m.idMaterial) " +
                    "inner join Laboratorio l on (e.idLaboratorio = l.idLaboratorio) where e.estatus = 1", sqlCon);
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

        public static List<Material> ObtenerMateriales()
        {
            DataTable tabla = new DataTable();
            Material m;
            List<Material> materiales = new List<Material>();
            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from material", sqlCon);
                sqlDa.Fill(tabla);

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    m = new Material();

                    m.IdMaterial = Convert.ToInt32(tabla.Rows[i]["idMaterial"]);
                    m.ClaveMaterial = Convert.ToString(tabla.Rows[i]["claveMaterial"]);
                    m.Nombre = Convert.ToString(tabla.Rows[i]["nombre"]);
                    m.Descripcion = Convert.ToString(tabla.Rows[i]["descripcion"]);
                    m.CostoDevolucion = Convert.ToSingle((tabla.Rows[i]["costoDevolucion"]));
                    m.Fotografia = Convert.ToString(tabla.Rows[i]["fotografia"]);
                    //System.Diagnostics.Debug.WriteLine(m.ToString());

                    materiales.Add(m);
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
            return materiales;
        }

        internal static Ejemplar ObtenerEjemplar(int id)
        {
            DataTable tabla = new DataTable();
            Ejemplar ejemplar;
            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            List<Material> materiales = ObtenerMateriales();
            List<Laboratorio> labs = ObtenerLabs();

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la calse conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from ejemplar where idEjemplar = @_id", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@_id", id);
                sqlDa.Fill(tabla);

                ejemplar = new Ejemplar();

                ejemplar.IdEjemplar = Convert.ToInt32(tabla.Rows[0][0]);
                ejemplar.ClaveEjemplar = Convert.ToString(tabla.Rows[0][1]);
                ejemplar.FechaCompra = Convert.ToDateTime(tabla.Rows[0][2]);
                ejemplar.Estatus = Convert.ToInt32(tabla.Rows[0][3]);
                ejemplar.Prestado = Convert.ToInt32(tabla.Rows[0][4]);

                //Obtenemos el objeto con el id de el value de nuestra vista
                ejemplar.Laboratorio = labs.Find(l => l.IdLaboratorio == Convert.ToInt32(tabla.Rows[0][5]));
                ejemplar.Material = materiales.Find(m => m.IdMaterial == Convert.ToInt32(tabla.Rows[0][6]));


            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            { // Este codigo se va a ejecutar aunque haya alguna excepcion. **SIEMPRE SE CERRARÁ LA CONEXIÓN**

                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return ejemplar;
        }

        public static List<Laboratorio> ObtenerLabs()
        {
            DataTable tabla = new DataTable();
            Laboratorio l;
            List<Laboratorio> labs = new List<Laboratorio>();
            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from laboratorio where estatus = 1", sqlCon);
                sqlDa.Fill(tabla);

                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    l = new Laboratorio();

                    l.IdLaboratorio = Convert.ToInt32(tabla.Rows[i]["idLaboratorio"]);
                    l.ClaveLaboratorio = Convert.ToString(tabla.Rows[i]["claveLaboratorio"]);
                    l.Nombre = Convert.ToString(tabla.Rows[i]["nombre"]);
                    l.Estatus = Convert.ToInt32(tabla.Rows[i]["estatus"]);
                    //System.Diagnostics.Debug.WriteLine(l.ToString());

                    labs.Add(l);
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
            return labs;
        }

        public static string InsertarEjemplar(Ejemplar ejemplar)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_insertarEjemplar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_fechaCompra", SqlDbType.Date).Value = ejemplar.FechaCompra;
                command.Parameters.Add("@var_idMaterial", SqlDbType.Int).Value = ejemplar.Material.IdMaterial;
                command.Parameters.Add("@var_idLaboratorio", SqlDbType.Int).Value = ejemplar.Laboratorio.IdLaboratorio;

                //Agregamos los parametros de salida (@var_idEjemplar)
                SqlParameter var_idEjemplar = new SqlParameter();
                var_idEjemplar.ParameterName = "@var_idEjemplar";
                var_idEjemplar.SqlDbType = SqlDbType.Int;
                var_idEjemplar.Direction = ParameterDirection.Output;

                //Agregamos los parametros de salida (claveCarrera)
                SqlParameter var_claveEjemplar = new SqlParameter();
                var_claveEjemplar.ParameterName = "@var_claveEjemplar";
                var_claveEjemplar.SqlDbType = SqlDbType.VarChar;
                var_claveEjemplar.Size = 30;
                var_claveEjemplar.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(var_idEjemplar);
                command.Parameters.Add(var_claveEjemplar);

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 respresenta un resultado exitoso (1 row affected)
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = "Ejemplar de" + ejemplar.Material.Nombre + " insertado correctamente. " +
                        "\nClave generada: " + Convert.ToString(var_claveEjemplar.Value);
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

        public static string ActualizarEjemplar(Ejemplar ejemplar)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarEjemplar", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idEjemplar", SqlDbType.Int).Value = ejemplar.IdEjemplar;
                command.Parameters.Add("@var_fechaCompra", SqlDbType.Date).Value = ejemplar.FechaCompra;
                command.Parameters.Add("@var_estatus", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@var_prestado", SqlDbType.Int).Value = ejemplar.Prestado;
                command.Parameters.Add("@var_idLaboratorio", SqlDbType.Int).Value = ejemplar.Laboratorio.IdLaboratorio;
                command.Parameters.Add("@var_idMaterial", SqlDbType.Int).Value = ejemplar.Material.IdMaterial;

                //Agregamos los parametros de salida (@var_idEjemplar)
                SqlParameter salida = new SqlParameter();
                salida.ParameterName = "@var_salidaConfirmacion";
                salida.SqlDbType = SqlDbType.Int;
                salida.Direction = ParameterDirection.Output;

                //Abrimos la conexion y guardamos el resultado en respuesta
                command.Parameters.Add(salida);

                sqlConnection.Open();

                if (command.ExecuteNonQuery() >= 1) // el 1 respresenta un resultado exitoso (1 row affected)
                {
                    //Esto quiere decir que se ingresó el provedor correctamente
                    respuesta = "Ejemplar de" + ejemplar.Material.Nombre + " actualizado correctamente. " +
                        "\nSalida: " + Convert.ToString(salida.Value);
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

        public static string BajaEjemplar(int id)
        {
            string respuesta = "";
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection = Conexion.getInstancia().CrearConexion();
                SqlCommand command = new SqlCommand("sp_actualizarEjemplarEstatus", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                //Agregamos los parametros:
                command.Parameters.Add("@var_idEjemplar", SqlDbType.Int).Value = id;

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
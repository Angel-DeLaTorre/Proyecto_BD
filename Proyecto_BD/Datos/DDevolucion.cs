using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Proyecto_BD.Models;

namespace Proyecto_BD.Datos
{
    public class DDevolucion
    {
        public static void realizarDevolucion(List<int> idEjemplares)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                using (sqlCon = Conexion.getInstancia().CrearConexion())
                {
                    sqlCon.Open();
                    using (SqlCommand cmdSP = new SqlCommand("sp_realizarDevolucion", sqlCon))
                    {
                        cmdSP.CommandType = CommandType.StoredProcedure;

                        using (var table = new DataTable())
                        {
                            table.Columns.Add("idEjemplarActual", typeof(int));

                            foreach (int idEjemplarActual in idEjemplares)
                            {
                                table.Rows.Add(idEjemplarActual);
                            }

                            //Se definen los parámetros
                            cmdSP.Parameters.Add("@var_idPrestamo", SqlDbType.Int).Value = 1;
                            cmdSP.Parameters.Add("@var_idEjemplares", SqlDbType.Structured).Value = table;
                            //Se define un TypeName de tipo type_idEjemplar para el parámetro de tipo tabla
                            cmdSP.Parameters["@var_idEjemplares"].TypeName = "dbo.type_idEjemplar";

                            cmdSP.Parameters.Add("@var_observaciones", SqlDbType.VarChar, 100).Value = "No pues no";

                            cmdSP.Parameters.Add("@var_salidaConfirmacion", SqlDbType.Int).Direction = ParameterDirection.Output;

                            cmdSP.ExecuteNonQuery();

                            //Se recuperan los valores de salida
                            int validacion = Convert.ToInt32(cmdSP.Parameters["@var_salidaConfirmacion"].Value);

                            Console.WriteLine("Validación " + validacion);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Excepción " + e.ToString());
            }
        }

        public static List<Object> getEjemplaresPrestados(string id)
        {
            List<Object> listDetalle = new List<Object>();
            try
            {
                SqlConnection sqlCon = new SqlConnection();

                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select e.idEjemplar,p.nombre,p.apPaterno,p.apMaterno,hp.clavePrestamo,hp.fechaPrestamo,hp.fechaLimite,m.nombre as material,m.claveMaterial from HistorialPrestamo hp " +
                        "inner join DetallePrestamo dp on hp.idPrestamo = dp.idPrestamo " +
                        "inner join Alumno a on hp.idAlumno = a.idAlumno " +
                        "inner join Persona p on p.idPersona = a.idPersona " +
                        "inner join Ejemplar e on e.idEjemplar = dp.idEjemplar " +
                        "inner join Material m on m.idMaterial = e.idMaterial " +
                        "where a.matricula = '" + id + "'";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        bool bandera = true;
                        List<Ejemplar> listEjemplar = new List<Ejemplar>();
                        if (reader.HasRows == false)
                        {
                            string msg = "No hay datos";
                            listDetalle.Add(msg);
                            return listDetalle;
                        }
                        while (reader.Read())
                        {
                            if (bandera)
                            {
                                bandera = false;
                                Prestamo prestamo = new Prestamo();
                                Persona p = new Persona();
                                Alumno a = new Alumno();

                                prestamo.clavePresramo = reader.GetString(reader.GetOrdinal("clavePrestamo"));
                                prestamo.fechaPrestamo = reader.GetDateTime(reader.GetOrdinal("fechaPrestamo"));
                                prestamo.fechaLimite = reader.GetDateTime(reader.GetOrdinal("fechaLimite"));
                                p.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                                p.apPaterno = reader.GetString(reader.GetOrdinal("apPaterno"));
                                p.apMaterno = reader.GetString(reader.GetOrdinal("apMaterno"));
                                a.Persona = p;
                                prestamo.Alumno = a;
                                listDetalle.Add(prestamo);
                            }
                            Ejemplar ejemplar = new Ejemplar();
                            Material m = new Material();
                            ejemplar.IdEjemplar = reader.GetInt32(reader.GetOrdinal("idEjemplar"));
                            m.Nombre = reader.GetString(reader.GetOrdinal("material"));
                            m.ClaveMaterial = reader.GetString(reader.GetOrdinal("claveMaterial"));
                            ejemplar.Material = m;
                            listEjemplar.Add(ejemplar);
                        }
                        listDetalle.Add(listEjemplar);
                    }
                }
                return listDetalle;
            }
            catch (Exception exc)
            {
                string msg = "Error al conectar con la base";
                listDetalle.Add(msg);
                Console.WriteLine("Error : " + exc.Message);
                return listDetalle;
            }
        }
    }
}
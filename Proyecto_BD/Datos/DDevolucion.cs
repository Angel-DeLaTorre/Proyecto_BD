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

        public void realizarDevolucion(List<int> idEjemplares)
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

        public List<string> getEjemplaresPrestados()
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
                    command.CommandText = @"select hp.clavePrestamo, p.nombre, p.apPaterno,p.apMaterno, c.nombre as carrera, g.nombre as grupo, hp.fechaPrestamo, hp.fechaLimite  from HistorialPrestamo hp "
                        + "join Alumno a on hp.idAlumno = a.idAlumno inner join Persona p on a.idPersona = p.idPersona "
                        + "inner join Carrera c on a.idCarrera = c.idCarrera "
                        + "inner join Grupo g on a.idGrupo = g.idGrupo where DATEDIFF(DAY,hp.fechaLimite,(SELECT GETDATE())) >= 3";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            listDetalle = null;
                            return listDetalle;
                        }
                        while (reader.Read())
                        {
                            Prestamo prestamo = new Prestamo();
                            Persona p = new Persona();
                            Alumno a = new Alumno();
                            Grupo g = new Grupo();
                            Carrera c = new Carrera();
                            prestamo.clavePresramo = reader.GetString(reader.GetOrdinal("clavePrestamo"));
                            p.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                            p.apPaterno = reader.GetString(reader.GetOrdinal("apPaterno"));
                            p.apMaterno = reader.GetString(reader.GetOrdinal("apMaterno"));
                            c.Nombre = reader.GetString(reader.GetOrdinal("carrera"));
                            g.Nombre = reader.GetString(reader.GetOrdinal("grupo"));
                            prestamo.fechaPrestamo = reader.GetDateTime(reader.GetOrdinal("fechaPrestamo"));
                            prestamo.fechaLimite = reader.GetDateTime(reader.GetOrdinal("fechaLimite"));
                            prestamo.observaciones = reader.GetString(reader.GetOrdinal("grupo"));
                            p.telefono = reader.GetString(reader.GetOrdinal("carrera"));
                            a.Persona = p;
                            prestamo.Alumno = a;
                            listPrestamos.Add(prestamo);
                        }
                    }
                }
                return listPrestamos;
            }
            catch (Exception exc)
            {
                listPrestamos = null;
                return listPrestamos;
            }


        }
    }
}
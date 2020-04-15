using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Datos
{
    public class DPrestamo
    {
        public static List<Ejemplar> ListarMaterialesEjemplares()
        {
            SqlConnection sqlCon = new SqlConnection();

            //Lista de alumnos
            List<Ejemplar> ejemplares = new List<Ejemplar>();

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT M.idMaterial, M.claveMaterial, M.nombre, M.descripcion, E.idEjemplar, E.claveEjemplar FROM Material M INNER JOIN Ejemplar E ON (M.idMaterial = E.idMaterial) WHERE E.estatus = 1 AND prestado = 1 ORDER BY M.idMaterial", sqlCon);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Material m = new Material();
                        Ejemplar e = new Ejemplar();

                        m.IdMaterial = reader.GetInt32(0); ;
                        m.ClaveMaterial = reader.GetString(1);
                        m.Nombre = reader.GetString(2);
                        m.Descripcion = reader.GetString(3);

                        e.IdEjemplar = reader.GetInt32(4);
                        e.ClaveEjemplar = reader.GetString(5);

                        e.Material = m;

                        ejemplares.Add(e);
                    }
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
            return ejemplares;


        }

        public static List<Laboratorista> ListarLaboratoristas()
        {
            SqlConnection sqlCon = new SqlConnection();

            //Lista de alumnos
            List<Laboratorista> laboratoristas = new List<Laboratorista>();

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT E.idLaboratorista, E.claveLaboratorista, E.turno, P.nombre, P.apPaterno, P.apMaterno, L.idLaboratorio, L.claveLaboratorio, L.nombre FROM Laboratorista E INNER JOIN Persona P ON (E.idPersona = P.idPersona) INNER JOIN Laboratorio L ON (E.idLaboratorio = L.idLaboratorio) WHERE E.estatus = 1", sqlCon);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Laboratorio l = new Laboratorio();
                        Persona p = new Persona();
                        Laboratorista e = new Laboratorista();

                        e.idLaboratorista = reader.GetInt32(0);
                        e.claveLaboratorista = reader.GetString(1);
                        e.turno = reader.GetString(2);

                        p.nombre = reader.GetString(3);
                        p.apPaterno = reader.GetString(4);
                        p.apMaterno = reader.GetString(5);

                        l.IdLaboratorio = reader.GetInt32(6);
                        l.ClaveLaboratorio = reader.GetString(7);
                        l.Nombre = reader.GetString(8);
                        e.Persona = p;
                        e.Laboratorio = l;


                        laboratoristas.Add(e);
                    }
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
            return laboratoristas;


        }

        public static List<Alumno> ListarAlumnos()
        {
            SqlConnection sqlCon = new SqlConnection();

            //Lista de alumnos
            List<Alumno> alumnos = new List<Alumno>();

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT A.idAlumno, A.matricula, P.nombre, P.apPaterno, P.apMaterno FROM Alumno A INNER JOIN Persona P ON(A.idPersona = P.idPersona) WHERE A.idAlumno NOT IN(SELECT idAlumno FROM HistorialPrestamo WHERE fechaDevolucion IS NULL OR pagoMulta = 0) AND A.estatus = 1", sqlCon);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Alumno a = new Alumno();
                        Persona p = new Persona();

                        a.idAlumno = reader.GetInt32(0);
                        a.matricula = reader.GetString(1);
                        p.nombre = reader.GetString(2);
                        p.apPaterno = reader.GetString(3);
                        p.apMaterno = reader.GetString(4);

                        a.Persona = p;

                        alumnos.Add(a);
                    }
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
            return alumnos;


        }

        public static DataTable ListarPrestamos()
        {

            DataTable tabla = new DataTable();

            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                SqlDataAdapter sqlDa =
                    new SqlDataAdapter("SELECT P.idPrestamo, P.clavePrestamo, L.claveLaboratorio, E.claveLaboratorista, A.matricula, P.fechaPrestamo, P.fechaLimite, P.fechaDevolucion, P.observaciones, P.estatus FROM HistorialPrestamo P INNER JOIN Laboratorio L ON (P.idLaboratorio = L.idLaboratorio) INNER JOIN Laboratorista E ON (P.idLaboratorista = E.idLaboratorista) INNER JOIN Alumno A ON (P.idAlumno = A.idAlumno)", sqlCon);

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


        public static string[] realizarPrestamo(Prestamo p, List<int> idEjemplares)
        {
            SqlConnection sqlCon = new SqlConnection(); // Con este objeto hacemos al conexion a la base de datos
            string[] respuesta;
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion(); //Utilizamos la variable tipo sql connection que obtenemos desde la clase conexion
                sqlCon.Open();

                using (SqlCommand cmdSP = new SqlCommand("sp_realizarPrestamo", sqlCon))
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
                        cmdSP.Parameters.Add("@var_idEjemplares", SqlDbType.Structured).Value = table;
                        cmdSP.Parameters["@var_idEjemplares"].TypeName = "dbo.type_idEjemplar";

                        cmdSP.Parameters.Add("@var_idLaboratorio", SqlDbType.Int).Value = p.Laboratorio.IdLaboratorio;
                        cmdSP.Parameters.Add("@var_idLaboratorista", SqlDbType.Int).Value = p.Laboratorista.idLaboratorista;
                        cmdSP.Parameters.Add("@var_idAlumno", SqlDbType.Int).Value = p.Alumno.idAlumno;
                        cmdSP.Parameters.Add("@var_fechaLimite", SqlDbType.Date).Value = p.FechaLimite;

                        cmdSP.Parameters.Add("@var_idPrestamo", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmdSP.Parameters.Add("@var_clavePrestamo", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        cmdSP.Parameters.Add("@var_validacionEjecucion", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmdSP.ExecuteNonQuery();


                        //Se recuperan los valores de salida
                        p.IdPrestamo = Convert.ToInt32(cmdSP.Parameters["@var_idPrestamo"].Value);
                        p.ClavePrestamo = Convert.ToString(cmdSP.Parameters["@var_clavePrestamo"].Value);
                        int validacion = Convert.ToInt32(cmdSP.Parameters["@var_validacionEjecucion"].Value);

                        respuesta = new string [] {Convert.ToString(validacion), p.ClavePrestamo};

                    }

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

            return respuesta;
        }

    }
}
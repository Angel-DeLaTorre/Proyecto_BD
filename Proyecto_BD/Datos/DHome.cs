using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Proyecto_BD.Models;

namespace Proyecto_BD.Datos 
{
    public class DHome
    {
        public static List<Prestamo> ListarAlumnosPendientes3()
        {
            List<Prestamo> listPrestamos = new List<Prestamo>();
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
                            listPrestamos = null;
                            return listPrestamos;
                        }
                        while (reader.Read())
                        {
                            Prestamo prestamo = new Prestamo();
                            Persona p = new Persona();
                            Alumno a = new Alumno();
                            Grupo g = new Grupo();
                            Carrera c = new Carrera();
                            prestamo.ClavePrestamo = reader.GetString(reader.GetOrdinal("clavePrestamo"));
                            p.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                            p.apPaterno = reader.GetString(reader.GetOrdinal("apPaterno"));
                            p.apMaterno = reader.GetString(reader.GetOrdinal("apMaterno"));
                            c.Nombre = reader.GetString(reader.GetOrdinal("carrera"));
                            g.Nombre = reader.GetString(reader.GetOrdinal("grupo"));
                            prestamo.FechaPrestamo = reader.GetDateTime(reader.GetOrdinal("fechaPrestamo")).ToString();
                            prestamo.FechaLimite = reader.GetDateTime(reader.GetOrdinal("fechaLimite")).ToString();
                            prestamo.Observaciones = reader.GetString(reader.GetOrdinal("grupo"));
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

        public static int countPrestamosRetrasos()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                int count = 0;
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select Count(hp.clavePrestamo) as countPrestamo from HistorialPrestamo hp "
                        + "join Alumno a on hp.idAlumno = a.idAlumno inner join Persona p on a.idPersona = p.idPersona "
                        + "inner join Carrera c on a.idCarrera = c.idCarrera "
                        + "inner join Grupo g on a.idGrupo = g.idGrupo where DATEDIFF(DAY,hp.fechaLimite,(SELECT GETDATE())) >= 3";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows == false)
                        {
                            return count;
                        }
                        while (reader.Read())
                        {
                            count = reader.GetInt32(reader.GetOrdinal("countPrestamo"));
                        }
                    }
                }
                return count;
            }
            catch (Exception exc)
            {
                return 0;
            }

        }

        public static List<string[]> ListarExistenciaLab(string id)
        {
            List<string[]> list = new List<string[]>();
            try
            {
                SqlConnection sqlCon = new SqlConnection();

                String where = "";
                if (!id.Equals("Todos"))
                {
                    where = " and l.claveLaboratorio = '" + id + "'";
                }

                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select l.claveLaboratorio, l.nombre, m.nombre as material, count(e.claveEjemplar) as existencias from Laboratorio l "+
                        "inner join Ejemplar e on e.idLaboratorio = l.idLaboratorio " +
                        "inner join Material m on m.idMaterial = e.idMaterial " +
                        "group by l.claveLaboratorio,m.nombre, e.prestado, l.nombre, l.estatus " +
                        "having e.prestado = 1 and l.estatus = 1 " + where;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            string[] item = new string[1];
                            item[0] = "No hay resultados";
                            list.Add(item);
                            return list;
                        }
                        while (reader.Read())
                        {
                            string[] item = new string[4];

                            item[0] = reader.GetString(reader.GetOrdinal("claveLaboratorio"));
                            item[1] = reader.GetString(reader.GetOrdinal("nombre"));
                            item[2] = reader.GetString(reader.GetOrdinal("material"));
                            item[3] = reader.GetInt32(reader.GetOrdinal("existencias")).ToString();

                            list.Add(item);
                        }
                    }
                }
                return list;
            }
            catch (Exception exc)
            {
                string[] item = new string[1];
                item[0] = "Error al consultar los datos";
                Console.WriteLine(exc.Message);
                list.Add(item);
                return list;
            }
        }

        public static int countMateriales() {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                int count = 0;
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select count(m.idMaterial) as countMaterial from Material m " +
                        "inner join Ejemplar e on m.idMaterial = e.idEjemplar where e.estatus = 1 or e.estatus = 2";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        if (reader.HasRows == false)
                        {
                            return count;
                        }
                        while (reader.Read())
                        {
                            count = reader.GetInt32(reader.GetOrdinal("countMaterial"));
                        }
                    }
                }
                return count;
            }
            catch (Exception exc)
            {
                return 0;
            }

        }

        public static List<Laboratorio> listLaboratorios()
        {
            List<Laboratorio> list = new List<Laboratorio>();
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                int count = 0;
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select claveLaboratorio,nombre from Laboratorio where estatus = 1";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows == false)
                        {
                            list = null;
                            return list;
                        }
                        while (reader.Read())
                        {
                            Laboratorio l = new Laboratorio();
                            l.ClaveLaboratorio = reader.GetString(reader.GetOrdinal("claveLaboratorio"));
                            l.Nombre = reader.GetString(reader.GetOrdinal("nombre"));
                            list.Add(l);
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

        public static List<string[]> ListarReporte(string date, string lab)
        {
            List<string[]> listPrestamos = new List<string[]>();
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    string query = "";
                    if (date.Equals("")) 
                    {
                        if (!lab.Equals("todos"))
                        {
                            query = "having l.claveLaboratorio = '" + lab + "'";
                        }
                    }
                    else
                    {
                        if (lab.Equals("todos"))
                        {
                            query = "having CAST(hp.fechaPrestamo as DATE) = '" + date + "'";
                        }
                        else
                        {
                            query = "having CAST(hp.fechaPrestamo as DATE) = '" + date + "' and l.claveLaboratorio = '" + lab + "'";
                        }
                    }

                    command.CommandText = @"select l.nombre as laboratorio, m.nombre as material, count(e.claveEjemplar) as Prestamos,hp.fechaPrestamo from HistorialPrestamo hp " +
                        "inner join DetallePrestamo dp on hp.idPrestamo = dp.idPrestamo " +
                        "inner join Ejemplar e on e.idEjemplar = dp.idEjemplar " +
                        "inner join Material m on m.idMaterial = e.idMaterial " +
                        "inner join Laboratorio l on l.idLaboratorio = hp.idLaboratorio " +
                        "group by m.claveMaterial, m.nombre, hp.fechaPrestamo, l.claveLaboratorio, l.nombre " + query + "  ORDER BY l.claveLaboratorio DESC";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            string[] item = new string[1];
                            item[0] = "No hay resultados";
                            listPrestamos.Add(item);
                            return listPrestamos;
                        }
                        while (reader.Read())
                        {
                            string[] item = new string[4];
                            item[0] = reader.GetString(reader.GetOrdinal("laboratorio"));
                            item[1] = reader.GetString(reader.GetOrdinal("material"));
                            item[2] = reader.GetInt32(reader.GetOrdinal("Prestamos")).ToString();
                            item[3] = reader.GetDateTime(reader.GetOrdinal("fechaPrestamo")).ToString();
                            listPrestamos.Add(item);
                        }
                    }
                }
                return listPrestamos;
            }
            catch (Exception exc)
            {
                string[] item = new string[1];
                item[0] = "Error al consultar los datos";
                Console.WriteLine(exc.Message);
                listPrestamos.Add(item);
                return listPrestamos;
            }
        }

        public static int countMaterialBaja()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                int count = 0;
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select count(m.idMaterial) as countMaterial from Material m " +
                        "inner join Ejemplar e on m.idMaterial = e.idMaterial where e.prestado = 3";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows == false)
                        {
                            return count;
                        }
                        while (reader.Read())
                        {
                            count = reader.GetInt32(reader.GetOrdinal("countMaterial"));
                        }
                    }
                }
                return count;
            }
            catch (Exception exc)
            {
                return 0;
            }
        }

        public static List<string[]> ListarBajasLab(string id)
        {
            List<string[]> list = new List<string[]>();
            try
            {
                SqlConnection sqlCon = new SqlConnection();

                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                using (SqlCommand command = sqlCon.CreateCommand())
                {
                    String where = "";
                    if (!id.Equals("Todos"))
                    {
                        where = " and l.claveLaboratorio = '" + id + "'";
                    }

                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"select l.claveLaboratorio, l.nombre, m.nombre as material, count(e.claveEjemplar) as bajas from Laboratorio l " +
                        "inner join Ejemplar e on e.idLaboratorio = l.idLaboratorio " +
                        "inner join Material m on m.idMaterial = e.idMaterial " +
                        "group by l.claveLaboratorio,m.nombre, e.prestado, l.nombre, l.estatus " +
                        "having e.prestado = 3 " + where;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            string[] item = new string[1];
                            item[0] = "No hay resultados";
                            list.Add(item);
                            return list;
                        }
                        while (reader.Read())
                        {
                            string[] item = new string[4];

                            item[0] = reader.GetString(reader.GetOrdinal("claveLaboratorio"));
                            item[1] = reader.GetString(reader.GetOrdinal("nombre"));
                            item[2] = reader.GetString(reader.GetOrdinal("material"));
                            item[3] = reader.GetInt32(reader.GetOrdinal("bajas")).ToString();

                            list.Add(item);
                        }
                    }
                }
                return list;
            }
            catch (Exception exc)
            {
                string[] item = new string[1];
                item[0] = "Error al consultar los datos";
                Console.WriteLine(exc.Message);
                list.Add(item);
                return list;
            }
        }
    }
}
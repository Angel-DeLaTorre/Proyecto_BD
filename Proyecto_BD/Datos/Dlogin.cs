using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Datos
{
    public class DLogin
    {
         /*List<string> los = new List<string>();
         public static List<string> ValidarUsuario(string user, string pass)
         {
             SqlDataReader a = null;
             SqlConnection sqlConnection = new SqlConnection();
             try
             {
                 sqlConnection = Conexion.getInstancia().CrearConexion();
                 SqlCommand command = new SqlCommand("sp_AccesoSistema", sqlConnection);
                 command.CommandType = CommandType.StoredProcedure;
                 //Agregamos los parametros:
                 command.Parameters.Add("@var_usuario", SqlDbType.VarChar).Value = user;
                 command.Parameters.Add("@var_password", SqlDbType.VarChar).Value = pass;
                 sqlConnection.Open();

                 a = command.ExecuteReader();

                 //los.fi

             }
             catch (Exception e)
             {
                 a = null;
             }
             finally
             {
                 if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
             }
             return a;
         }*/
    }
}
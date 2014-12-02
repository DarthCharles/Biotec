using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Biotec
{
    class Conexion
    {

        MySqlConnection connection;


        public Conexion()
        {
            try
            {
                string bdadmin = ReadVarAppConfig("bdpass");
                string dirser = ReadVarAppConfig("dirserver");
                connection = new MySqlConnection();
                connection.ConnectionString = "Server=" + dirser + "; Database=laboratorio; Uid=root; Pwd='" + bdadmin + "'";
                connection.Open();
            }
            catch { }
        }

        public static string ReadVarAppConfig(string nombreVar)
        {
            //leer una variable de app.config
            string resultado = null;
            try
            {
                resultado = System.Configuration.ConfigurationManager.AppSettings[nombreVar];
            }
            catch (Exception)
            {
                resultado = null;
            }
            return resultado;
        }
        public void Close()
        {
            connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }

        public string count()
        {
            try
            {


                string sql = "select count(*) from clientes";
                MySqlCommand busqueda = new MySqlCommand(sql, connection);
                busqueda.ExecuteNonQuery();
                MySqlDataReader reader = busqueda.ExecuteReader();
                reader.Read();
                string lol = reader.GetString(0);
                reader.Close();
                return lol;

            }
            catch { return "0"; }
        }

        public void insertar(string tabla, ArrayList parametros, ArrayList values)
        {

            string sql = "insert into " + tabla + " (";

            for (int i = 0; i < parametros.Count; i++)
            {
                if (i != parametros.Count - 1)
                {
                    sql += parametros[i] + ",";
                }
                else
                {
                    sql += parametros[i];
                }
            }
            sql += ") values (";


            for (int i = 0; i < values.Count; i++)
            {
                if (i != values.Count - 1)
                {
                    sql += "'" + values[i] + "'" + ",";
                }
                else
                {
                    sql += "'" + values[i] + "'";
                }
            }
            sql += ");";

            MySqlCommand busqueda = new MySqlCommand(sql, connection);
            busqueda.ExecuteNonQuery();
        }

        public void insertar(string tabla, string[] parametros, string[] values)
        {
            string sql = "insert into " + tabla + " (";

            for (int i = 0; i < parametros.Count(); i++)
            {
                if (i != parametros.Count() - 1)
                {
                    sql += parametros[i] + ",";
                }
                else
                {
                    sql += parametros[i];
                }
            }
            sql += ") values (";


            for (int i = 0; i < values.Count(); i++)
            {
                if (i != values.Count() - 1)
                {
                    sql += "'" + values[i] + "'" + ",";
                }
                else
                {
                    sql += "'" + values[i] + "'";
                }
            }
            sql += ");";

            MySqlCommand busqueda = new MySqlCommand(sql, connection);
            busqueda.ExecuteNonQuery();
        }

        public string leer1(string parametro, string tabla, string where)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string sql = "select " + parametro + " from " + tabla + " where idanalisis = '" + where + "'";
            MySqlCommand busqueda = new MySqlCommand(sql, connection);
            MySqlDataReader reader = busqueda.ExecuteReader();
            reader.Read();
            return reader.GetString(0);

        }
        public ArrayList leer(string parametro, string tabla)
        {
            ArrayList contenido = new ArrayList();
            string sql = "select " + parametro + " from " + tabla;

            MySqlCommand busqueda = new MySqlCommand(sql, connection);
            MySqlDataReader reader = busqueda.ExecuteReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                contenido.Add(reader.GetString(i));
            }
            reader.Close();
            return contenido;
        }

        public ArrayList leer2(string tabla, string parametro)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            ArrayList contenido = new ArrayList();
            string sql = "select * from " + tabla + " where idAnalisis = " + parametro;
            MySqlCommand busqueda = new MySqlCommand(sql, connection);
            MySqlDataReader reader = busqueda.ExecuteReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                contenido.Add(reader.GetString(i));
            }
            reader.Close();
            return contenido;
        }

        public void update(string tabla, ArrayList parametros, ArrayList values, string idAnalisis, string doctor)
        {
            string sql1 = "update documento set doctor = '" + doctor + "' where idanalisis = " + idAnalisis;
            MySqlCommand busqueda1 = new MySqlCommand(sql1, connection);
            busqueda1.ExecuteNonQuery();

            string sql = "delete from " + tabla + " where idAnalisis = '" + idAnalisis + "';";
            MySqlCommand busqueda = new MySqlCommand(sql, connection);
            busqueda.ExecuteNonQuery();
            parametros.Add("idanalisis");
            values.Add(idAnalisis);

            insertar(tabla, parametros, values);
        }

        public void delete(string tabla, string id)
        {
            string sql = "delete from " + tabla + " where idAnalisis = '" + id + "';";
            MySqlCommand busqueda = new MySqlCommand(sql, connection);
            busqueda.ExecuteNonQuery();
        }

    }
}

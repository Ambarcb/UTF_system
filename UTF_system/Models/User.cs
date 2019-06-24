using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace UTF_system.Models
{
    public class User
    {
        public enum Tipo{admin = 1, profesor = 2, estudiante = 3}
            
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public Tipo Type { get; set; }

        public User(int id, string nombre, string apellido, string password, Tipo tipo)
        {
            this.ID = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Password = password;
            this.Type = tipo;
        }

        public static User SelectUserById(int id)
        {
                string connectionString = ConfigurationManager.ConnectionStrings["db_connection"].ConnectionString;
               
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string query = String.Format("SELECT * FROM users_table WHERE id = {0}", id);

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader dr = sqlCommand.ExecuteReader();

                User user = null;

                if (dr.Read())
                {
                
                    string nombre = dr["nombre"].ToString();
                    string apellido = dr["apellido"].ToString();
                    string password = dr["password"].ToString();
                    Tipo tipo = (Tipo)int.Parse(dr["tipo"].ToString());

                    user = new User(id, nombre, apellido, password, tipo);
                
                
                }

                else
                    return null;

                return user;

        }
    }
}
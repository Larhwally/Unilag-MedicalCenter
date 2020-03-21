using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Models
{
    public class DbAccessLayer
    {
        //public string ConnectionString = "server=localhost;port=3306;database=unilag_medic;user=root;password=ellnerd22";
        MySqlConnection SqlConnection = new MySqlConnection("server=localhost;port=3306;database=unilag_medic;user=root;password=ellnerd22");

        public string AddCredential(UnilagMedLogin unilag)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand( "INSERT INTO tbl_userlogin(email, password) VALUES(@email, @password)", SqlConnection);
                cmd.Parameters.AddWithValue("@email", unilag.email);
                cmd.Parameters.AddWithValue("@password", unilag.password);
                SqlConnection.Open();
                int n = cmd.ExecuteNonQuery();
                SqlConnection.Close();
                return (n > 0).ToString();
            }
            catch (Exception ex)
            {
                if (SqlConnection.State == System.Data.ConnectionState.Open)
                {
                    SqlConnection.Close();
                }
                return (ex.Message);
            }
        }
    }
}

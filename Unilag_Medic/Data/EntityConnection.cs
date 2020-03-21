<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
=======
﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
<<<<<<< HEAD
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Unilag_Medic.ViewModel;
=======
using System.Text;
using System.Threading.Tasks;
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6

namespace Unilag_Medic.Data
{
    public class EntityConnection
    {
<<<<<<< HEAD
        public string ConnectionString = "server=localhost;port=3306;database=unilag_medic;user=root;password=ellnerd22";
        private MySqlConnection connection;
=======
        public string ConnectionString = "Server=DESKTOP-FL7SFF4;Database=Unilag_Medic;User ID=sa;Password=ellnerd22;Trusted_Connection=True;";
        private SqlConnection connection;
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
        private string tableName;
        private int defaultSelectLength;
        private Dictionary<string, string> tableSchema;

        public static string ToJson(List<Dictionary<string, object>> list) //changed object to string for dict
        {
            //string result = "";
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Dictionary<string, object>>)); //changed object to string for dict

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    serializer.WriteObject(ms, list);
            //    result = Encoding.Default.GetString(ms.ToArray());
            //}
            var JsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return JsonResult;
        }

        public EntityConnection(string tableName)
        {
            this.defaultSelectLength = 100;
            this.tableName = tableName;
            this.loadConnection();
            this.tableSchema = this.GetTableSchema();
        }
        
      

<<<<<<< HEAD
        internal MySqlConnection loadConnection()
        {
            if(this.connection == null)
            {
                this.connection = new MySqlConnection(this.ConnectionString);
=======
        internal SqlConnection loadConnection()
        {
            if(this.connection == null)
            {
                this.connection = new SqlConnection(this.ConnectionString);
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            }
            else
            {
                this.connection.Close();
            }
            return this.connection;
        }

        private Dictionary<string, string> GetTableSchema()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string query = "select column_name, data_type from information_schema.columns where table_name='" + this.tableName + "'";
            this.connection.Open();
<<<<<<< HEAD
            MySqlCommand command = new MySqlCommand(query, this.connection);
=======
            SqlCommand command = new SqlCommand(query, this.connection);
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6

            List<Dictionary<string, object>> tempResult = BaseSelect(query); //changed object to string for dict
            for (int i = 0; i < tempResult.Count; i++)
            {
                Dictionary<string, object> current = tempResult[i]; //changed object to string for dict
                result.Add(current["column_name"].ToString(), current["data_type"].ToString());
            }
            return result;

        }


        public EntityConnection(string tableName, int defaultlength):this(tableName)
        {
            this.defaultSelectLength = defaultlength;
        }

        //select all
        public List<Dictionary<string, object>> Select() 
        {
            string query = "select * from " + this.tableName + " ";
            return this.BaseSelect(query);
        }
        private List<Dictionary<string, object>> BaseSelect(string query) //changed object to string for dict
        {
            if (this.connection.State == ConnectionState.Closed)
            {
                this.connection.Open();

            }
<<<<<<< HEAD
            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
=======
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>(); //changed object to string for dict
            while (reader.Read())
            {
                Dictionary<string, object> tempResult = new Dictionary<string, object>(); // changed object to string for dict
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    tempResult.Add(reader.GetName(i), reader.GetValue(i).ToString());
                }
               
                result.Add(tempResult);
            }
            reader.Close();
            this.connection.Close();
            return result;
        }

        //my method for returning a value after register
        public string ResponseVal()
        {
            connection.Open();
            Dictionary<string, object> tempResult = new Dictionary<string, object>();
            foreach (var item in tempResult)
            {
                var i = item.Value;
                var j = item.Key;
            }
            connection.Close();
            return tempResult.Values.ToString();
        }

        public List<Dictionary<string, object>> Select(int start, int length) //changed object to string for dict
        {
            string query = "select * from " + this.tableName + "  limit " + start + "," + length;
            return this.BaseSelect(query);
        }
        public List<Dictionary<string, object>> Select(int start) //changed object to string for dict
        {
            int length = this.defaultSelectLength;
            string query = "select * from " + this.tableName + "  limit " + start + "," + length;
            return this.BaseSelect(query);
        }

        private static string GetPlaceholder(string[] keys)
        {
            string result = "";
            for (int i = 0; i < keys.Length; i++)
            {
                string currentValue = "@" + keys[i];
                result += string.IsNullOrEmpty(result) ? currentValue : "," + currentValue;
            }
            return result;
        }
        ////add new column to tables
        //public bool AddColumn(string newColumn)
        //{
        //    this.connection.Open();
        //    string query = "alter table " + this.tableName + " add column_name, data_type from information.schema " + newColumn;
        //    SqlCommand command = new SqlCommand(query, this.connection);
            
        //}

        //insert method
        public bool Insert(Dictionary<string, string> content)
        {
            this.connection.Open();
            string[] keys = content.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);
            string query = " insert into " + this.tableName + "(" + implode(keys) + ") values (" + placeholder + ")";

<<<<<<< HEAD
            MySqlCommand command = new MySqlCommand(query, this.connection);
=======
            SqlCommand command = new SqlCommand(query, this.connection);
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            for (int i = 0; i < keys.Length; i++)
            {
                string currentParam = "@" + keys[i];
                string currentValue = content[keys[i]].ToString();
<<<<<<< HEAD
                MySqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                MySqlParameter tempParam = new MySqlParameter(currentParam, dbType);
=======
                SqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                SqlParameter tempParam = new SqlParameter(currentParam, dbType);
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
                tempParam.Value = wrapValue(currentValue, dbType);
                command.Parameters.Add(tempParam);
            }
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
<<<<<<< HEAD
           
        }

       
        private object wrapValue(string currentValue, MySqlDbType dbType)
        {
            if (dbType == MySqlDbType.DateTime)
=======
            //connection.Open();
            //string query="insert into "+this.tableName

        }

        private object wrapValue(string currentValue, SqlDbType dbType)
        {
            if (dbType == SqlDbType.DateTime)
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            {
                DateTime datetime = DateTime.Parse(currentValue);
                return datetime;
            }
<<<<<<< HEAD
            if (dbType == MySqlDbType.Time)
=======
            if (dbType == SqlDbType.Time)
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            {
                TimeSpan datetime = TimeSpan.Parse(currentValue);
                return datetime;
            }
           
            return currentValue;
        }


<<<<<<< HEAD
        private MySqlDbType getColumnType(string v)
=======
        private SqlDbType getColumnType(string v)
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
        {
            v = v.ToLower();
            switch (v)
            {
                case "int":
<<<<<<< HEAD
                    return MySqlDbType.Int32;
                case "varchar":
                    return MySqlDbType.VarChar;
                case "datetime":
                    return MySqlDbType.DateTime;
                case "time":
                    return MySqlDbType.Time;
                case "decimal":
                    return MySqlDbType.Decimal;
                case "text":
                    return MySqlDbType.Text;
                case "nvarchar":
                    return MySqlDbType.LongText;
                default:
                    return MySqlDbType.JSON;
=======
                    return SqlDbType.Int;
                case "varchar":
                    return SqlDbType.VarChar;
                case "datetime":
                    return SqlDbType.DateTime;
                case "time":
                    return SqlDbType.Time;
                case "decimal":
                    return SqlDbType.Decimal;
                case "text":
                    return SqlDbType.Text;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                default:
                    return SqlDbType.Udt;
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            }
        }

        //to connect two joins into a single string
        private static string implode(string[] array)
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                string currentValue = array[i];
                result += string.IsNullOrEmpty(result) ? currentValue : "," + currentValue; //another way of writing if-then-else
            }
            return result;
        }

        //public string ResponseMsg(bool resp)
        //{
        //    if (resp == true)
        //    {
        //        string result = "Record successfully created!";
        //    }
        //    else
        //    {
        //        string result = "Error in saving record!";
        //    }

        //    return resp.ToString();
        //}

        //Select by parameter
        public List<Dictionary<string, object>> SelectByColumn(Dictionary<string, string> queryParam)
        {
            string query = "select * from " + this.tableName + " ";
            string param = "";
            string[] keys = queryParam.Keys.ToArray<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                string currentKey = keys[i];
                string valueString = queryParam[currentKey];
                param += string.IsNullOrWhiteSpace(param) ? " where " : " and ";
                param += (currentKey + " = " + valueString);
            }
            return this.BaseSelect(query + param);
        }

        //Update by ID 
        public bool Update(int id, Dictionary<string, string> content)
        {
            this.connection.Open();
            string[] keys = content.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);
            string query = " update  " + this.tableName + " set ";
<<<<<<< HEAD
            MySqlCommand command = new MySqlCommand();
=======
            SqlCommand command = new SqlCommand();
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            for (int i = 0; i < keys.Length; i++)
            {
                string currentKey = keys[i];
                query += (i > 0 && keys.Length > 0 ? "," : "") + " " + currentKey + " = @" + currentKey;
                string currentParam = "@" + keys[i];
                string currentValue = content[keys[i]];
<<<<<<< HEAD
                MySqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                MySqlParameter tempParam = new MySqlParameter(currentParam, dbType);
=======
                SqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                SqlParameter tempParam = new SqlParameter(currentParam, dbType);
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
                tempParam.Value = wrapValue(currentValue, dbType);
                command.Parameters.Add(tempParam);
            }
            string query2 = " where itbId =@itbId";

            command.CommandText = query + query2;
<<<<<<< HEAD
            MySqlParameter parameter = new MySqlParameter("@itbId", MySqlDbType.Int32);
=======
            SqlParameter parameter = new SqlParameter("@itbId", SqlDbType.Int);
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            parameter.Value = id;
            command.Parameters.Add(parameter);
            command.Connection = this.connection;
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
            //connection.Open();
            //string query="insert into "+this.tableName
        }

        //Delete
        public bool Delete(Dictionary<string, string> queryParam)
        {
            this.connection.Open();
            string query = "delete from " + this.tableName + " ";
            string param = "";
            string[] keys = queryParam.Keys.ToArray<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                string currentKey = keys[i];
                string valueString = queryParam[currentKey];
                param += string.IsNullOrWhiteSpace(param) ? " where " : " and ";
                param += (currentKey + " = " + valueString);
            }
            query += param;
<<<<<<< HEAD
            MySqlCommand command = new MySqlCommand(query, this.connection);
=======
            SqlCommand command = new SqlCommand(query, this.connection);
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }

<<<<<<< HEAD
        public bool AddUser(UnilagMedLogin unilag)
        {
            this.connection.Open();
            string query = "INSERT INTO tbl_userlogin(email, password) VALUES(@email,@password)";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@email", unilag.email);
            cmd.Parameters.AddWithValue("@password", unilag.password);
            int n = cmd.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }

        public bool CheckUser(string email, string password)
        {
            this.connection.Open();
            bool hasRows = false;
            string query = "SELECT * FROM tbl_userlogin WHERE email = @email  AND password = @password";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            byte[] salt = { 2, 3, 1, 2, 3, 6, 7, 4, 2, 3, 1, 7, 8, 9, 6 };
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
                ));
            password = hashed;
            
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            hasRows = dataReader.HasRows;
            this.connection.Close();
            return hasRows;
        }

=======
>>>>>>> 96f04cc51e2c16fc308e11a56920f8ef785501e6


    }


}

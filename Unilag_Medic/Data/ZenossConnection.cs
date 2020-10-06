using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Unilag_Medic.Models;

namespace Unilag_Medic.Data
{
    public class ZenossConnection
    {
        public string ConnectionString = "server=localhost;port=3306;database=zenoss_db;user=lawal;password=password00";
        private MySqlConnection connection;
        private string tableName;
        private Dictionary<string, string> tableSchema;


        public ZenossConnection(string tableName)
        {
            this.tableName = tableName;
            this.loadConnection();
            this.tableSchema = this.GetTableSchema();
        }

        private Dictionary<string, string> GetTableSchema()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string query = "select column_name, data_type from information_schema.columns where table_name='" + this.tableName + "'";
            this.connection.Open();
            MySqlCommand command = new MySqlCommand(query, this.connection);

            List<Dictionary<string, object>> tempResult = BaseSelect(query); //changed object to string for dict
            for (int i = 0; i < tempResult.Count; i++)
            {
                Dictionary<string, object> current = tempResult[i]; //changed object to string for dict
                result.TryAdd(current["column_name"].ToString(), current["data_type"].ToString());
            }
            return result;
        }

        private List<Dictionary<string, object>> BaseSelect(string query)
        {
            if (this.connection.State == ConnectionState.Closed)
            {
                this.connection.Open();

            }
            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
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

        internal MySqlConnection loadConnection()
        {
            if (this.connection == null)
            {
                this.connection = new MySqlConnection(this.ConnectionString);
            }
            else
            {
                this.connection.Close();
            }
            return this.connection;
        }

        private object wrapValue(string currentValue, MySqlDbType dbType)
        {
            if (dbType == MySqlDbType.DateTime)
            {
                DateTime datetime = DateTime.Parse(currentValue);
                return datetime;
            }
            if (dbType == MySqlDbType.Time)
            {
                TimeSpan datetime = TimeSpan.Parse(currentValue);
                return datetime;
            }

            return currentValue;
        }

        private MySqlDbType getColumnType(string v)
        {
            v = v.ToLower();
            switch (v)
            {
                case "int":
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
            }
        }

        private static string implode(string[] keys)
        {
            string result = "";
            for (int i = 0; i < keys.Length; i++)
            {
                string currentValue = keys[i];
                result += string.IsNullOrEmpty(result) ? currentValue : "," + currentValue; //another way of writing if-then-else
            }
            return result;
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



        //Select by ID parameter from all tables
        public Dictionary<string, object> SelectByColumn(Dictionary<string, string> queryParam)
        {
            this.connection.Open();
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
            MySqlCommand cmd = new MySqlCommand(query + param, this.connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            Dictionary<string, object> result = new Dictionary<string, object>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.Add(reader.GetName(i), reader.GetValue(i));
                }
            }
            reader.Close();
            this.connection.Close();
            return result;
            //return this.BaseSelect(query + param);
        }











    }
}
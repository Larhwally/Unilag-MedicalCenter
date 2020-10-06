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
        public string ConnectionString = "server=localhost;port=3306;database=tbl_zenossdetails;user=root;password=";
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

        // Amethod to insert staff details fetched from Zenoss API
        public string InsertZenossStaff(IEnumerable<StaffModel> staffModels)
        {
            this.connection.Open();
            string query = "INSERT INTO tbl_zenoss_staffs(nameTitle, staffId, institutionEmail, lastName, firstName, otherName, dateOfBirth, stateOfOrigin, maritalStatus, dateOfFirstAppointment, nationality, telephone, mobile, address, createDate) " +
                           "VALUES(@nameTitle, @staffId, @institutionEmail, @lastName, @firstName, @otherName, @dateOfBirth, @stateOfOrigin, @maritalStatus, @dateOfFirstAppointment, @nationality, @telephone, @mobile, @address, @createDate)";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            DateTime createDate = DateTime.Now;

            foreach (var staff in staffModels)
            {
                command.Parameters.AddWithValue("@nameTitle", staff.NameTitle);
                command.Parameters.AddWithValue("@staffId", staff.StaffID);
                command.Parameters.AddWithValue("@institutionEmail", staff.InstitutionEmail);
                command.Parameters.AddWithValue("@lastName", staff.LastName);
                command.Parameters.AddWithValue("@firstName", staff.FirstName);
                command.Parameters.AddWithValue("@otherName", staff.OtherName);
                command.Parameters.AddWithValue("@dateOfBirth", staff.DateOfBirth);
                command.Parameters.AddWithValue("@stateOfOrigin", staff.StateOfOrigin);
                command.Parameters.AddWithValue("@maritalStatus", staff.MaritalStatus);
                command.Parameters.AddWithValue("@dateOfFirstAppointment", staff.DateOfFirstAppointment);
                command.Parameters.AddWithValue("@nationality", staff.Nationality);
                command.Parameters.AddWithValue("@telephone", staff.Telephone);
                command.Parameters.AddWithValue("@mobile", staff.Mobile);
                command.Parameters.AddWithValue("@address", staff.Address);
                command.Parameters.AddWithValue("@createDate", createDate);

                int n = command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            this.connection.Close();
            return "Inserted";
        }

        //Insert method for session token 
        public string InsertSessionToken(string zenosLogin)
        {
            this.connection.Open();
            string query = "INSERT INTO tbl_zenoss_sessiontoken(session_token, createdBy) VALUES(@session_token, @createdBy)";

            string createdBy = "lawal";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@session_token", zenosLogin);
            command.Parameters.AddWithValue("@createdBy", createdBy);
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return "Inserted";
        }

        // Insert method
        public bool Insert(Dictionary<string, object> content)
        {
            this.connection.Open();
            string[] keys = content.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);
            string query = "insert into " + this.tableName + "(" + implode(keys) + ") values (" + placeholder + ")";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            for (int i = 0; i < keys.Length; i++)
            {
                string currentParam = "@" + keys[i];
                string currentValue = content[keys[i]].ToString();
                MySqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                MySqlParameter tempParam = new MySqlParameter(currentParam, dbType);
                tempParam.Value = wrapValue(currentValue, dbType);
                command.Parameters.Add(tempParam);
            }
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;

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

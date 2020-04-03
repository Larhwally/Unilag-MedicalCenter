using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Data
{
    public class EntityConnection
    {
        public string ConnectionString = "server=localhost;port=3306;database=unilagmedicdb;user=lawal;password=password00";
        private MySqlConnection connection;
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
        
      

        internal MySqlConnection loadConnection()
        {
            if(this.connection == null)
            {
                this.connection = new MySqlConnection(this.ConnectionString);
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
            MySqlCommand command = new MySqlCommand(query, this.connection);

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


        //BaseSelect method
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

        public List<Dictionary<string, object>> Select(int start, int length) 
        {
            string query = "select * from " + this.tableName + "  limit " + start + "," + length;
            return this.BaseSelect(query);
        }
        public List<Dictionary<string, object>> Select(int start) 
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
        

        //insert method
        public bool Insert(Dictionary<string, string> content)
        {
            this.connection.Open();
            string[] keys = content.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);
            string query = " insert into " + this.tableName + "(" + implode(keys) + ") values (" + placeholder + ")";

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

        
        //Select by ID parameter
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

        //select by hospitalNumber parameter
        public List<Dictionary<string, object>> SelectByParam(Dictionary<string, string> queryvalues)
        {
            this.connection.Open();
            string query = "select * from " + this.tableName + " ";
            string parameter = "";
            string[] keys = queryvalues.Keys.ToArray<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                string currentKey = keys[i];
                string valueString = queryvalues[currentKey];
                parameter += string.IsNullOrWhiteSpace(parameter) ? " where " : " and ";
                parameter += (currentKey + " = " + "\"" + valueString + "\"");
            }
            return this.BaseSelect(query + parameter);
        }


        /*Update by ID 
         This  is a general method for all  tables, I  would need to write  a method like this that uses hospitalnumber as the search parameter
         for tables who have hospitalnumber as a common key (tbl_patient,  tbl_visit, tbl_vital, and other lab form tables)
             */
        public bool Update(int id, Dictionary<string, string> content)
        {
            this.connection.Open();
            string[] keys = content.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);
            string query = " update  " + this.tableName + " set ";
            MySqlCommand command = new MySqlCommand();
            for (int i = 0; i < keys.Length; i++)
            {
                string currentKey = keys[i];
                query += (i > 0 && keys.Length > 0 ? "," : "") + " " + currentKey + " = @" + currentKey;
                string currentParam = "@" + keys[i];
                string currentValue = content[keys[i]];
                MySqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                MySqlParameter tempParam = new MySqlParameter(currentParam, dbType);
                tempParam.Value = wrapValue(currentValue, dbType);
                command.Parameters.Add(tempParam);
            }
            string query2 = " where itbId =@itbId";

            command.CommandText = query + query2;
            MySqlParameter parameter = new MySqlParameter("@itbId", MySqlDbType.Int32);
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
            MySqlCommand command = new MySqlCommand(query, this.connection);
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }

        public bool AddUser(UnilagMedLogin unilag)
        {
            this.connection.Open();
            string query = "INSERT INTO tbl_userlogin(email, password, roleId, medstaffId, createBy, createDate) VALUES(@email,@password,@roleId,@medstaffId,@createBy,@createDate)";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@email", unilag.email);
            cmd.Parameters.AddWithValue("@password", unilag.password);
            cmd.Parameters.AddWithValue("@roleId", unilag.roleId);
            cmd.Parameters.AddWithValue("@medstaffId", unilag.medstaffId);
            cmd.Parameters.AddWithValue("@createBy", unilag.createBy);
            cmd.Parameters.AddWithValue("@createDate", unilag.createDate);
            int n = cmd.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }

        public bool UpdateUser(int id, UnilagMedLogin unilag)
        {
            this.connection.Open();
            string query = "UPDATE tbl_userlogin SET password = @password, createBy = @createBy, createDate = @createDate WHERE itbId = @itbId";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlParameter parameter = new MySqlParameter("@itbId", MySqlDbType.Int32);
            byte[] salt = { 2, 3, 1, 2, 3, 6, 7, 4, 2, 3, 1, 7, 8, 9, 6 };
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: unilag.password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 50
                ));
            unilag.password = hashed;
            parameter.Value = id;
            command.Parameters.Add(parameter);
            command.Parameters.AddWithValue("@password", unilag.password);
            command.Parameters.AddWithValue("@createBy", unilag.createBy);
            command.Parameters.AddWithValue("@createDate", unilag.createDate);
            int n = command.ExecuteNonQuery();
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
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 50
                ));
            password = hashed;
            
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            hasRows = dataReader.HasRows;
            this.connection.Close();
            return hasRows;
        }

        //Use this method to get the roleId of the currently logged in user email 
        public string SelectRole(int role, string email)
        {
            this.connection.Open();
            string query = "SELECT roleId FROM tbl_userlogin WHERE email = @email";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@roleId", role);
            MySqlDataReader reader = cmd.ExecuteReader();
            bool hasrow = reader.HasRows;
            if (reader.Read())
            {
                role = Convert.ToInt32(reader["roleId"].ToString());
            }
            reader.Close();
            this.connection.Close();
            return role +"";
        }

        //Use this method to join userlogin & role to get roletitle
        public List<Dictionary<string, object>> DisplayRoles(string email)
        {
            this.connection.Open();
            string query = "SELECT email, roleId, tbl_role.itbId, roleTitle FROM tbl_userlogin INNER JOIN tbl_role ON tbl_userlogin.roleId = tbl_role.itbId WHERE email = @email";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@email", email);
            MySqlDataReader reader = command.ExecuteReader();
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> tempresult = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    tempresult.Add(reader.GetName(i), reader.GetValue(i));
                }
                result.Add(tempresult);
            }
            reader.Close();
            this.connection.Close();
            return result;
        }

        //Use this  method to get a  patient visit record with  hospital number as parameter
        public List<Dictionary<string, object>> DisplayVisitValues(string hospitalNumber)
        {
            this.connection.Open();
            string query = "SELECT hospitalNumber, patientTypeName, visitTypeName, visitDateTime, clinicName, staffCode, position FROM tbl_visit " +
                            "INNER JOIN tbl_patient ON tbl_visit.patientId = tbl_patient.itbId " +
                            "INNER JOIN tbl_patienttype ON tbl_visit.patientType = tbl_patienttype.itbId " +
                            "INNER JOIN tbl_visittype ON tbl_visit.visitTypeId = tbl_visittype.itbId " +
                            "INNER JOIN tbl_clinic ON tbl_visit.clinicId = tbl_clinic.itbId " +
                            "INNER JOIN tbl_medicalstaff ON tbl_visit.recordStaffId = tbl_medicalstaff.itbId WHERE hospitalNumber = @hospitalNumber";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@hospitalNumber", hospitalNumber);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> tempdata = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    tempdata.Add(reader.GetName(i), reader.GetValue(i));
                }
                values.Add(tempdata);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        //Use this method to get VItal signs with hospital number as search parameter
        public List<Dictionary<string, object>> DisplayVitalValues(string hospnum)
        {
            this.connection.Open();
            string query = "SELECT  hospitalNumber, tbl_patient.surname, tbl_patient.gender, bloodPressure, temperature, pulse, bmiStatus, visitDateTime, staffCode, position, tbl_vitalsigns.createDate FROM tbl_vitalsigns " +
                            "INNER JOIN tbl_patient ON tbl_vitalsigns.patientId = tbl_patient.itbId  " +
                            "INNER  JOIN tbl_visit ON tbl_vitalsigns.visitId = tbl_visit.itbId " +
                            "INNER JOIN tbl_medicalstaff ON tbl_vitalsigns.nurseId  =  tbl_medicalstaff.itbId WHERE hospitalNumber = @hospitalNumber ";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@hospitalNumber", hospnum);
            MySqlDataReader reader = command.ExecuteReader();
            List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    pairs.Add(reader.GetName(i), reader.GetValue(i));
                }
                values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        public List<Dictionary<string, object>> DisplayDiagnosis(string hospnum)
        {
            this.connection.Open();
            string query = "SELECT hospitalNumber, tbl_patient.surname, complaints, examination, diagnosis, prescription, tbl_visit.visitDateTime, " +
                            "bloodPressure, temperature, pulse FROM tbl_diagnosis " +
                            "INNER JOIN tbl_patient ON tbl_diagnosis.patientId = tbl_patient.itbId " +
                            "INNER JOIN tbl_visit ON tbl_diagnosis.visitId  = tbl_visit.itbId " +
                            "INNER JOIN tbl_vitalsigns  ON  tbl_diagnosis.vitalId = tbl_vitalsigns.itbId WHERE hospitalNumber = @hospitalNumber";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@hospitalNumber", hospnum);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    pairs.Add(reader.GetName(i), reader.GetValue(i));
                }
                values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }
        





    }


}

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
using Unilag_Medic.Models;
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

        public static string ToJson(string list) //changed object to string for dict
        {

            //string result = "";
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Dictionary<string, object>>)); //changed object to string for dict

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    serializer.WriteObject(ms, list);
            //    result = Encoding.Default.GetString(ms.ToArray());
            //}
            JsonConvert.SerializeObject(list);
            return list;

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


        public EntityConnection(string tableName, int defaultlength) : this(tableName)
        {
            this.defaultSelectLength = defaultlength;
        }

        //select all with limit to 20 records
        public List<Dictionary<string, object>> Select()
        {
            string query = "select * from " + this.tableName + " ORDER by itbId DESC LIMIT 20 ";
            return this.BaseSelect(query);
        }


        // Select without limit of records
        public List<Dictionary<string, object>> SelectAll()
        {
            string query = "SELECT * FROM " + this.tableName + " ORDER BY itbId DESC";
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
        public bool Insert(Dictionary<string, object> content)
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


        //A second insert method for dictionary with string key and string value dictionary key-value pair
        public bool InsertRecord(Dictionary<string, string> content)
        {
            this.connection.Open();
            string[] keys = content.Keys.ToArray<string>();
            string placeHolder = GetPlaceholder(keys);
            string query = "INSERT INTO " + this.tableName + "(" + implode(keys) + ") VALUES (" + placeHolder + ");";

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


        //A method for insert that catch the primary key of new ine=serted record in the database
        public long InsertScalar(Dictionary<string, object> content)
        {
            this.connection.Open();
            string[] keys = content.Keys.ToArray<string>();
            string placeHolder = GetPlaceholder(keys);
            string query = "INSERT INTO " + this.tableName + "(" + implode(keys) + ") VALUES (" + placeHolder + ");";

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
            command.ExecuteScalar();
            long w = command.LastInsertedId;
            return w;
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

        //select by hospitalNumber parameter from tables  where  hospital  number exists as columns
        public Dictionary<string, object> SelectByParam(Dictionary<string, string> queryvalues)
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
                parameter += (currentKey + " = " + "'" + valueString + "'");
            }

            MySqlCommand cmd = new MySqlCommand(query + parameter, this.connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            Dictionary<string, object> result = new Dictionary<string, object>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.TryAdd(reader.GetName(i), reader.GetValue(i));
                }
            }
            reader.Close();
            this.connection.Close();
            return result;
            //return this.BaseSelect(query + parameter);
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
            string query = "INSERT INTO tbl_userlogin(email, password, roleId, medstaffId, updatedBy, createDate) VALUES(@email,@password,@roleId,@medstaffId,@updatedBy,@createDate)";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@email", unilag.email);
            cmd.Parameters.AddWithValue("@password", unilag.password);
            cmd.Parameters.AddWithValue("@roleId", unilag.roleId);
            cmd.Parameters.AddWithValue("@medstaffId", unilag.medstaffId);
            cmd.Parameters.AddWithValue("@updatedBy", unilag.createBy);
            cmd.Parameters.AddWithValue("@createDate", unilag.createDate);
            int n = cmd.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }

        public bool UpdateUser(int id, UnilagMedLogin unilag)
        {
            this.connection.Open();
            string query = "UPDATE tbl_medicalstaff SET password = @password, createDate = @createDate WHERE itbId = @itbId";
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
            command.Parameters.AddWithValue("@updatedBy", unilag.createBy);
            command.Parameters.AddWithValue("@createDate", unilag.createDate);
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }


        public bool CheckUser(string email, string password)
        {
            this.connection.Open();
            bool hasRows = false;
            string query = "SELECT * FROM tbl_medicalstaff WHERE email = @email  AND password = @password";
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
            return role + "";
        }

        //Use this method to join userlogin & role to get roletitle
        public Dictionary<string, object> DisplayRoles(string email)
        {
            this.connection.Open();
            string query = "SELECT tbl_medicalstaff.itbId AS medstaffId, staffCode, surname, tbl_medicalstaff.email, tbl_medicalstaff.roleId, roleTitle FROM tbl_medicalstaff " +
                            "INNER JOIN tbl_role ON tbl_medicalstaff.roleId = tbl_role.itbId WHERE tbl_medicalstaff.email = @email";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@email", email);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, object> result = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> tempresult = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.Add(reader.GetName(i), reader.GetValue(i));
                }
                //result.Add(tempresult);
            }
            reader.Close();
            this.connection.Close();
            return result;
        }


        //Select vital signs record using visit ID as a search parameter
        public Dictionary<string, object> SelectVitalByVisit(int visitId)
        {
            this.connection.Open();
            string query = "SELECT tbl_vitalsigns.itbId, tbl_vitalsigns.patientId, hospitalNumber, tbl_patient.surname, bpSystolic, bpDiastolic, bloodPressure, temperature," +
                            " pulse, bmi, bmiStatus, oxygenSaturation, otherNotes, tbl_vitalsigns.visitId, tbl_vitalsigns.status, nurseId, tbl_vitalsigns.createDate FROM tbl_vitalsigns " +
                            " INNER JOIN tbl_patient ON tbl_vitalsigns.patientId = tbl_patient.itbId " +
                            " INNER JOIN tbl_visit ON tbl_vitalsigns.visitId = tbl_visit.itbId WHERE visitId = @visitId";

            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@visitId", visitId);
            MySqlDataReader reader = cmd.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        //Use this  method to get a  patient visit record with  hospital number as parameter
        public List<Dictionary<string, object>> DisplayVisitValues(string hospitalNumber)
        {
            this.connection.Open();
            string query = "SELECT hospitalNumber, tbl_patient.otherNames, tbl_patient.gender, visitDateTime, clinicName, staffCode, tbl_medicalstaff.surname, position, tbl_visit.createDate FROM tbl_visit " +
                            "INNER JOIN tbl_patient ON tbl_visit.patientId = tbl_patient.itbId " +
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
            string query = "SELECT  hospitalNumber, tbl_patient.surname, tbl_patient.gender, bloodPressure, temperature, pulse, bmiStatus, visitDateTime, staffCode, tbl_medicalstaff.otherNames, position, assignedTo, tbl_vitalsigns.createDate FROM tbl_vitalsigns " +
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

        //Use this method for searching vital signs with hospital  number
        public List<Dictionary<string, object>> DisplayDiagnosis(string hospnum)
        {
            this.connection.Open();
            string query = "SELECT hospitalNumber, tbl_patient.surname, complaints, examination, diagnosis, tbl_visit.visitDateTime, " +
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


        //Select Diagnosis record using visitID as a search parameter
        public Dictionary<string, object> SelectDiagnosisByVisit(int visitId)
        {
            this.connection.Open();
            string query = "SELECT * FROM tbl_diagnosis WHERE visitId = @visitId";

            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@visitId", visitId);
            MySqlDataReader reader = cmd.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.TryAdd(reader.GetName(i), reader.GetValue(i));
                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }




        //Use this method to search through student patients using matric number as search parameter
        public Dictionary<string, object> StudentPatient(string matricNum)
        {
            this.connection.Open();
            string query = "SELECT matricNumber, hospitalNumber, surname, otherNames, nhisNumber, gender, phoneNumber, email, tbl_patient.status FROM tbl_student_patient " +
                            "INNER JOIN tbl_patient ON tbl_student_patient.patientId = tbl_patient.itbId WHERE matricNumber = @matricNumber";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@matricNumber", matricNum);
            MySqlDataReader reader = cmd.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        public Dictionary<string, object> StaffPatient(string staffCode)
        {
            this.connection.Open();
            string query = "SELECT staffCode, hospitalNumber, surname, otherNames, nhisNumber, gender, phoneNumber, email, tbl_patient.status FROM tbl_staff_patient " +
                            "INNER JOIN tbl_patient ON tbl_staff_patient.patientId = tbl_patient.itbId WHERE staffCode = @staffCode";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@staffCode", staffCode);
            MySqlDataReader reader = cmd.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        public static string ConvertToJson(string result)
        {
            var JsonResult = JsonConvert.SerializeObject(result);
            return JsonResult;
        }

        //Check image unique name on the database
        public bool CheckImage(string uniquePath)
        {
            this.connection.Open();
            bool hasRows = false;
            string query = "select * from " + this.tableName + " where pictureId =  @pictureId  ";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("pictureId", uniquePath);
            MySqlDataReader reader = cmd.ExecuteReader();
            hasRows = reader.HasRows;
            this.connection.Close();
            return hasRows;


        }

        //Select all visit record specifying including the fields added
        public List<Dictionary<string, object>> SelectAllVisit()
        {
            this.connection.Open();
            string query = "SELECT tbl_visit.itbId, hospitalNumber, tbl_patient.surname, tbl_patient.otherNames, tbl_patient.gender, tbl_patient.dateOfBirth, patientType, " +
                            "clinicName, visitDateTime, tbl_visit.recordStaffId, staffCode, tbl_medicalstaff.email, tbl_visit.status, tbl_visit.createDate, vitalStatus, tbl_visit.assignedTo  FROM tbl_visit" +
                            " INNER JOIN tbl_patient ON tbl_visit.patientId = tbl_patient.itbId" +
                            " INNER JOIN tbl_clinic ON tbl_visit.clinicId = tbl_clinic.itbId" +
                            " INNER JOIN tbl_medicalstaff ON tbl_visit.recordStaffId = tbl_medicalstaff.itbId ORDER BY itbId DESC LIMIT 20";

            MySqlCommand command = new MySqlCommand(query, this.connection);
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


        //Select visit by ID
        public Dictionary<string, object> SelectVisitById(int id)
        {
            this.connection.Open();
            string query = "SELECT tbl_visit.itbId, tbl_visit.patientId, hospitalNumber, tbl_patient.surname, tbl_patient.otherNames, tbl_patient.gender, tbl_patient.dateOfBirth, patientType, " +
                            " tbl_visit.clinicId, clinicType, visitDateTime, tbl_visit.lastVisitId, tbl_visit.status, tbl_visit.createDate, vitalStatus  FROM tbl_visit" +
                            " INNER JOIN tbl_patient ON tbl_visit.patientId = tbl_patient.itbId" +
                            " INNER JOIN tbl_clinic ON tbl_visit.clinicId = tbl_clinic.itbId  where tbl_visit.itbId = @itbId";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("itbId", id);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }



        public List<Dictionary<string, object>> SelectAllVitalsigns()
        {
            this.connection.Open();
            string query = "SELECT tbl_vitalsigns.itbId, hospitalNumber, tbl_patient.surname, tbl_patient.otherNames, tbl_patient.gender, tbl_patient.dateOfBirth, patientType " +
                            "clinicId, visitId, visitDateTime, nurseId, staffcode, tbl_medicalstaff.email, tbl_visit.patientId, tbl_vitalsigns.assignedTo, tbl_vitalsigns.status, tbl_vitalsigns.createDate  FROM tbl_vitalsigns" +
                            " INNER JOIN tbl_patient ON tbl_vitalsigns.patientId = tbl_patient.itbId" +
                            " INNER JOIN tbl_visit ON tbl_vitalsigns.visitId = tbl_visit.itbId" +
                            " INNER JOIN tbl_medicalstaff ON tbl_vitalsigns.nurseId = tbl_medicalstaff.itbId LEFT JOIN tbl_doctor ON tbl_vitalsigns.assignedTo = tbl_doctor.itbId ORDER BY itbId DESC LIMIT 20";

            MySqlCommand command = new MySqlCommand(query, this.connection);
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

        //Method for last appointment details
        public Dictionary<string, object> LastVisit(int patientId)
        {
            this.connection.Open();
            //bool hasRows = false;

            string query = "SELECT tbl_visit.itbId, visitDateTime, clinicType, tbl_visit.assignedTo, tbl_medicalstaff.surname, tbl_medicalstaff.otherNames FROM tbl_visit" +
               " LEFT OUTER JOIN tbl_clinic ON tbl_visit.clinicId = tbl_clinic.itbId" +
               " LEFT OUTER JOIN tbl_medicalstaff ON tbl_visit.assignedTo = tbl_medicalstaff.itbId" +
               " where tbl_visit.patientId = @patientId ORDER BY visitDateTime DESC LIMIT 1";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@patientId", patientId);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        //Use this method to get daily appointments by passing date yyyy-mm-dd
        public List<Dictionary<string, object>> DailyVisit(string visitDate)
        {
            this.connection.Open();
            string query = "SELECT tbl_visit.itbId, hospitalNumber, tbl_patient.surname, tbl_patient.otherNames, tbl_patient.gender, tbl_patient.dateOfBirth, tbl_clinic.clinicName," +
                            " visitDateTime, lastVisitId, patientType, tbl_visit.recordStaffId, staffCode, tbl_medicalstaff.email, tbl_visit.status, tbl_visit.createDate, vitalStatus," +
                            " assignedTo, GROUP_CONCAT(tbl_medicalstaff.surname, tbl_medicalstaff.otherNames SEPARATOR', ') AS doctorName FROM tbl_visit" +
                            " LEFT JOIN tbl_patient ON tbl_visit.patientId = tbl_patient.itbId LEFT JOIN tbl_clinic ON tbl_visit.clinicId = tbl_clinic.itbId" +
                            " LEFT JOIN tbl_medicalstaff ON tbl_visit.assignedTo = tbl_medicalstaff.itbId WHERE visitDateTime LIKE " + "\"%" + visitDate + "%\" " + "group by tbl_visit.itbId";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@visitDateTime", visitDate);
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

        //Use this method to update vitalsign status on visit table 
        public bool UpdateVisit(int visitId, int assignedTo)
        {
            this.connection.Open();
            bool hasRow = false;
            string query = "UPDATE tbl_visit SET vitalStatus = 1, " + "assignedTo = " + assignedTo + " WHERE itbId = " + visitId;

            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@itbId", visitId);
            cmd.Parameters.AddWithValue("@assignedTo", assignedTo);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            hasRow = dataReader.HasRows;
            this.connection.Close();
            return hasRow;
        }


        //Use this method to update Diagnosis status on visit table 
        public bool UpdateDiagnosis(int visitId)
        {
            this.connection.Open();
            bool hasRow = false;
            string query = "UPDATE tbl_visit SET diagnosisStatus = 1  WHERE itbId = " + visitId;
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@itbId", visitId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            hasRow = dataReader.HasRows;
            this.connection.Close();
            return hasRow;
        }


        //Select appointed to details from tbl_vital using the visitId
        public Dictionary<string, object> SelectAppointedDetails(int id)
        {
            this.connection.Open();
            string query = "SELECT tbl_visit.itbId, assignedTo, tbl_medicalstaff.surname, tbl_medicalstaff.otherNames FROM tbl_visit" +
                           " INNER JOIN tbl_medicalstaff ON tbl_visit.assignedTo = tbl_medicalstaff.itbId WHERE tbl_visit.itbId = @visitId";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@visitId", id);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                //Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        //Select all doctors from tbl_doctor joining tbl_medicalstaff to get bio data
        public List<Dictionary<string, object>> GetDoctors()
        {
            this.connection.Open();
            string query = "SELECT tbl_medicalstaff.itbId, surname, otherNames, specializationName, tbl_medicalstaff.status, tbl_medicalstaff.createdBy, tbl_medicalstaff.createDate FROM tbl_medicalstaff" +
                            " LEFT JOIN tbl_specialization ON tbl_medicalstaff.specializationId = tbl_specialization.itbId WHERE roleId = 5";
            MySqlCommand command = new MySqlCommand(query, this.connection);
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

        //Select all apppointment assigned to a doctor by passing doctorID (assignedTo)
        public List<Dictionary<string, object>> DoctorsAppoinmentList(int assignedTo)
        {
            this.connection.Open();
            string query = "SELECT tbl_visit.itbId, visitDateTime, tbl_visit.patientId, tbl_visit.recordStaffId, tbl_visit.clinicId, vitalStatus, diagnosisStatus, assignedTo, surname, otherNames, lastVisitId, tbl_visit.status, prescriptionId  FROM `tbl_visit`" +
                            " INNER JOIN tbl_medicalstaff ON tbl_visit.assignedTo = tbl_medicalstaff.itbId WHERE assignedTo = " + assignedTo + " ORDER BY visitDateTime DESC ";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@assignedTo", assignedTo);
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



        // Search method for a drug search for prescription using typed value in the search bar as search parameter
        public List<Dictionary<string, object>> DrugSearch(string drugName)
        {
            this.connection.Open();
            string query = "SELECT * FROM tbl_druginventory WHERE drugName LIKE " + "\"%" + drugName + "%\"";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("drugName", drugName);
            MySqlDataReader reader = command.ExecuteReader();

            List<Dictionary<string, object>> keyValues = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    pairs.Add(reader.GetName(i), reader.GetValue(i));
                }
                keyValues.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return keyValues;
        }


        // Select lab test names from tbl_lab_test which are referenced in an array of lab test names during lab test request
        public List<Dictionary<string, object>> LabTestNames(string[] labTestName)
        {
            this.connection.Open();
            string query = "SELECT * FROM tbl_lab_test WHERE " + string.Join(" OR ", labTestName);
            MySqlCommand command = new MySqlCommand(query, this.connection);
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



        //Select drug detaiils from tbl_druginventory which are referenced in an array of drugs passed during prescription 
        public List<Dictionary<string, object>> DrugDetails(string[] drugArray)
        {
            this.connection.Open();
            string query = "SELECT * FROM `tbl_druginventory` WHERE " + string.Join(" OR ", drugArray);
            MySqlCommand command = new MySqlCommand(query, this.connection);
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



        //Insert a staff patient perculiar records besides the generic patient records
        public bool InsertStaffPatient(Dictionary<string, object> StaffDetail)
        {
            this.connection.Open();
            string[] keys = StaffDetail.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);
            string query = "INSERT INTO tbl_staff_patient (" + implode(keys) + ") VALUES(" + placeholder + ")";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            for (int i = 0; i < keys.Length; i++)
            {
                string currentParam = "@" + keys[i];
                string currentValue = StaffDetail[keys[i]].ToString();
                MySqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                MySqlParameter tempParam = new MySqlParameter(currentParam, dbType);
                tempParam.Value = wrapValue(currentValue, dbType);
                command.Parameters.Add(tempParam);
            }
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }


        // Insert a student patient perculiar record besides the generis patient record

        public bool InsertStudentPatient(Dictionary<string, object> student)
        {
            this.connection.Open();
            string[] keys = student.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);

            string query = "INSERT INTO tbl_student_patient (" + implode(keys) + ") VALUES(" + placeholder + ")";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            for (int i = 0; i < keys.Length; i++)
            {
                string currentParam = "@" + keys[i];
                string currentValue = student[keys[i]].ToString();
                MySqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                MySqlParameter tempParam = new MySqlParameter(currentParam, dbType);
                tempParam.Value = wrapValue(currentValue, dbType);
                command.Parameters.Add(tempParam);
            }
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }


        //Insert a non staff patient specific data to the non staff table besides the generic patient record

        public bool InsertNonStaff(Dictionary<string, object> nonStaff)
        {
            this.connection.Open();
            string[] keys = nonStaff.Keys.ToArray<string>();
            string placeholder = GetPlaceholder(keys);

            string query = "INSERT INTO tbl_nonstaff (" + implode(keys) + ") VALUES(" + placeholder + ")";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            for (int i = 0; i < keys.Length; i++)
            {
                string currentParam = "@" + keys[i];
                string currentValue = nonStaff[keys[i]].ToString();
                MySqlDbType dbType = getColumnType(this.tableSchema[keys[i]]);
                MySqlParameter tempParam = new MySqlParameter(currentParam, dbType);
                tempParam.Value = wrapValue(currentValue, dbType);
                command.Parameters.Add(tempParam);
            }
            int n = command.ExecuteNonQuery();
            this.connection.Close();
            return n > 0;
        }



        // Select a lab request record using Visit ID as a search parameter
        public Dictionary<string, object> SelectLabRequest(int visitId)
        {
            this.connection.Open();
            string query = "SELECT * FROM tbl_labtest_request WHERE visitId = " + visitId;
            MySqlCommand command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("visitId", visitId);
            MySqlDataReader reader = command.ExecuteReader();

            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
            }
            reader.Close();
            this.connection.Close();
            return values;
        }

        // Select staff schedule
        public List<Dictionary<string, object>> SelectAllStaffSchedule()
        {
            this.connection.Open();
            string query = "SELECT tbl_staff_schedule.staffId, staffCode, surname, otherNames, gender, phoneNumber, tbl_staff_schedule.roleId, " +
                           "tbl_staff_schedule.clinicId, clinicType, clinicName, scheduleDate FROM tbl_staff_schedule " +
                           "LEFT JOIN tbl_medicalstaff ON tbl_staff_schedule.staffId = tbl_medicalstaff.itbId " +
                           "LEFT JOIN tbl_clinic on tbl_staff_schedule.clinicId = tbl_clinic.itbId";

            MySqlCommand command = new MySqlCommand(query, this.connection);
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

        // Generate patient record report
        public Dictionary<string, object> PatientReport()
        {
            this.connection.Open();
            string query = "SELECT COUNT(tbl_patient.itbId) AS Total_Patient, SUM(CASE WHEN tbl_patient.patientType = 1 THEN 1 ELSE 0 END) AS staff_patient," +
                           " SUM(CASE WHEN tbl_patient.patientType = 2 THEN 1 ELSE 0 END)  AS student_patient, " +
                           "SUM(CASE WHEN tbl_patient.patientType = 4 THEN 1 ELSE 0 END) AS non_staff_patient FROM tbl_patient";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                // Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));

                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;

        }


        // Generate appointment record
        public Dictionary<string, object> AppointmentReport()
        {
            this.connection.Open();
            string query = "SELECT COUNT(tbl_visit.itbId) AS Total_Appointment, SUM(tbl_visit.vitalStatus = 0) AS Awaiting_vitals, " +
                            "SUM(tbl_visit.diagnosisStatus = 1) AS Attended FROM tbl_visit";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                // Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));

                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        // Generate report for medical staffs
        public Dictionary<string, object> GetStaffReport()
        {
            this.connection.Open();
            string query = "SELECT COUNT(tbl_medicalstaff.itbId) AS Total_Staffs, SUM(CASE WHEN tbl_medicalstaff.roleId = 4 THEN 1 ELSE 0 END) AS record_staff, " +
                           "SUM(CASE WHEN tbl_medicalstaff.roleId = 3 THEN 1 ELSE 0 END) AS nurses, SUM(CASE WHEN tbl_medicalstaff.roleId = 5 THEN 1 ELSE 0 END) AS doctors, " +
                           "SUM(CASE WHEN tbl_medicalstaff.roleId = 7 THEN 1 ELSE 0 END) AS pharmacists, SUM(CASE WHEN tbl_medicalstaff.roleId = 6 THEN 1 ELSE 0 END) AS lab_technician, " +
                           "SUM(CASE WHEN tbl_medicalstaff.roleId = 8 THEN 1 ELSE 0 END) AS xray_staffs FROM tbl_medicalstaff";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                // Dictionary<string, object> pairs = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));

                }
                //values.Add(pairs);
            }
            reader.Close();
            this.connection.Close();
            return values;
        }

        // Insert a result of an External API call into a table
        public string InsertResult(List<NationalityModel> countries)
        {

            this.connection.Open();
            string query = "INSERT INTO tbl_nationality(nationalityName, capitalName, regionName, subRegionName, createdBy, createDate) " +
                           "VALUES(@nationalityName, @capitalName, @regionName, @subRegionName, @createdBy, @createDate)";

            MySqlCommand command = new MySqlCommand(query, this.connection);
            string createdBy = "lawal";
            DateTime createDate = DateTime.Now;
            foreach (var country in countries)
            {
                command.Parameters.AddWithValue("@nationalityName", country.Name);
                command.Parameters.AddWithValue("@capitalName", country.Capital);
                command.Parameters.AddWithValue("@regionName", country.Region);
                command.Parameters.AddWithValue("@subRegionName", country.Subregion);
                command.Parameters.AddWithValue("@createdBy", createdBy);
                command.Parameters.AddWithValue("@createDate", createDate);
                int n = command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            this.connection.Close();
            return "Inserted";

        }


        // A method that is triggered by cron to change depedent status if age is above 18
        public bool UpdateDependentStatus()
        {
            var today = DateTime.Now;
            DateTime birthDate = new DateTime();
            var age = today.Year - birthDate.Year;

            bool hasRow = false;
            this.connection.Open();
            string query = "UPDATE tbl_dependent SET status = " + "Above 18" + " WHERE age > 18";
            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlDataReader dataReader = command.ExecuteReader();
            hasRow = dataReader.HasRows;
            this.connection.Close();
            return hasRow;


        }

        // Hospital number check
        public string GenerateUniqueHospitalNumber()
        {
            //bool hasRows = true;
            this.connection.Open();

            var notUnique = true;
            var hospnum = "";

            while (notUnique)
            {
                hospnum = Utility.GenerateHospNum();
                string query = "SELECT * FROM tbl_patient WHERE hospitalNumber = @hospitalNumber";
                MySqlCommand cmd = new MySqlCommand(query, this.connection);
                cmd.Parameters.AddWithValue("@hospitalNumber", hospnum);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                notUnique = dataReader.HasRows;
            }
            //hasRows = dataReader.HasRows;

            this.connection.Close();
            return hospnum;
        }


        // A method to get lab request data from the DB using the visitId as a filter paramater
        public Dictionary<string, object> GetLabRequestByVisit(int visitId)
        {
            this.connection.Open();
            string query = "SELECT * FROM tbl_labtest_request WHERE visitId = @visitId ORDER BY itbId DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@visitId", visitId);
            MySqlDataReader reader = cmd.ExecuteReader();

            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


        // A method to get X-ray request data from the DB using the visitId as a filter paramater
        public Dictionary<string, object> GetXrayRequestByVisit(int visitId)
        {
            this.connection.Open();
            string query = "SELECT * FROM tbl_xray_request WHERE visitId = @visitId ORDER BY itbId DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@visitId", visitId);
            MySqlDataReader reader = cmd.ExecuteReader();

            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
            }
            reader.Close();
            this.connection.Close();
            return values;
        }



        // A method to get Refereals request data from the DB using the visitId as a filter paramater
        public Dictionary<string, object> GetReferralByVisit(int visitId)
        {
            this.connection.Open();
            string query = "SELECT * FROM tbl_referral WHERE visitId = @visitId ORDER BY itbId DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@visitId", visitId);
            MySqlDataReader reader = cmd.ExecuteReader();

            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetName(i), reader.GetValue(i));
                }
            }
            reader.Close();
            this.connection.Close();
            return values;
        }

        // A meethod to get a prescription record by visitId
        public Dictionary<string, object> GetPrescriptionVisit(int visitId)
        {
            this.connection.Open();
            string query = "SELECT * from tbl_prescription WHERE visitId = @visitId ORDER BY itbId DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.Parameters.AddWithValue("@visitId", visitId);
            MySqlDataReader reader = cmd.ExecuteReader();

            Dictionary<string, object> values = new Dictionary<string, object>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.TryAdd(reader.GetName(i), reader.GetValue(i));
                }
            }
            reader.Close();
            this.connection.Close();
            return values;
        }


         // A method to update patient table with image unique path
        public bool UpdateImagePath(int patientId, string uniquePath)
        {
            this.connection.Open();
            bool hasRow = false;
            string query = "UPDATE tbl_patient SET pictureId = " + uniquePath + "WHERE itbId = " + patientId;
            MySqlCommand command = new MySqlCommand(query, this.connection);
            MySqlDataReader dataReader = command.ExecuteReader();
            hasRow = dataReader.HasRows;
            this.connection.Close();
            return hasRow;

        }








    }


}

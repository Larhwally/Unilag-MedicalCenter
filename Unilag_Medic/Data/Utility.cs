using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Unilag_Medic.Data
{
    public class Utility
    {
        // pick the data specified in the array out of the param dictionary containing full patient data and save it in a dictionary
        public static Dictionary<string, object> Pick(Dictionary<string, object> source, string[] fieldsNeeded)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            for (int i = 0; i < fieldsNeeded.Length; i++)
            {
                string field = fieldsNeeded[i];
                result.Add(field, source[field]);
            }

            return result;
        }

        // Hash a string such as password
        public static string Hash(string toBeHashed)
        {
            byte[] salt = { 2, 3, 1, 2, 3, 6, 7, 4, 2, 3, 1, 7, 8, 9, 6 };
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: toBeHashed,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 1000,
            numBytesRequested: 50
            ));

            return hashed;
        }

        // Generate a randon number hospital number 
        public static string GenerateHospNum()
        {
            Random random = new Random();
            int randomNumber = random.Next(10, 100000);
            var hospnum = "unimed-" + randomNumber;
            return hospnum;
        }


    }
}
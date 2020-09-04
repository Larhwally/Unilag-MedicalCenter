using System.Collections.Generic;

namespace Unilag_Medic.Data
{
    public class Utility
    {
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
    }
}
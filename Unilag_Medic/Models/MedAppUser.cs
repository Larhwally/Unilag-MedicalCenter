using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unilag_Medic.Models
{
    public class MedAppUser
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int medstaffid { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}

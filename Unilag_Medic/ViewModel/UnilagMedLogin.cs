using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unilag_Medic.ViewModel
{
    public class UnilagMedLogin
    {
        public string email { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
        public int medstaffId { get; set; }
        public string createBy { get; set; }
        public DateTime createDate { get; set; }
    }
}

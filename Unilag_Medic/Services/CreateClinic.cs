using System;
using System.Collections.Generic;
using Unilag_Medic.Data;

namespace Unilag_Medic.Services
{

    public class CreateClinic : ICreateClinic
    {
        public void InsertClinic()
        {
            EntityConnection connection = new EntityConnection("tbl_clinic");

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("clinicName", "HangFire");
            param.Add("clinicType", "HangFire");
            param.Add("clinicDescription", "HangFire");
            param.Add("clinicOpenSchedule", "1");
            param.Add("status", "HangFire");
            param.Add("createdBy", "HangFire");
            param.Add("createDate", DateTime.Now);

            connection.Insert(param);


        }
    }
}
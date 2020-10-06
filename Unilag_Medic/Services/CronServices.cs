using Unilag_Medic.Data;

namespace Unilag_Medic.Services
{

    public class CronServices : ICronServices
    {
        public void UpdateDependent()
        {
            EntityConnection connection = new EntityConnection("tbl_dependent");
            connection.UpdateDependentStatus();

        }
 

    }
}
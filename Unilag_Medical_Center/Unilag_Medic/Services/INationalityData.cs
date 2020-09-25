using System.Threading.Tasks;
using Unilag_Medic.Models;

namespace Unilag_Medic.Services
{
    public interface INationalityData
    {
        Task<NationalityModel> GetCountries();
    }
}
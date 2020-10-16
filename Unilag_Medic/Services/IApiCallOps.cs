namespace Unilag_Medic.Services
{
    public interface IApiCallOps
    {
        Task<List<string>> GetStateByCountry(string countryName, string token, int nationalityId);
    }
}
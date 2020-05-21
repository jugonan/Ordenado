using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Heldu.Logic.Services
{
    [ApiController]
    [Route("[controller]")]
    public class GeoLocationService : ControllerBase
    {
        private readonly WebServiceClient _maxMindClient;

        public GeoLocationService(WebServiceClient maxMindClient)
        {
            _maxMindClient = maxMindClient;
        }

        [HttpGet]
        public async Task<string> GetCountryNameAsync(string ip)
        {
            var location = await _maxMindClient.CountryAsync(ip);
            return location.Country.Name;
        }
    }
}

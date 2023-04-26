using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Square;
using Square.Apis;
using Square.Models;
using Microsoft.Extensions.Configuration;
namespace YourProjectName.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SquareController : ControllerBase
    {
        private readonly string _squareApplicationId;
        private readonly string _squareAccessToken;
        public SquareController(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _squareApplicationId = configuration.GetValue<string>("AppSettings:SquareApplicationId");
            _squareAccessToken = configuration.GetValue<string>("AppSettings:SquareAccessToken");
        }
        [HttpGet("locations")]
        public async Task<ActionResult> GetLocations()
        {
            var client = new SquareClient.Builder()
                .Environment(Square.Environment.Sandbox)
                .AccessToken(_squareAccessToken)
                .Build();
            var locationsApi = client.LocationsApi;
            var response = await locationsApi.ListLocationsAsync();
            var locations = response.Locations;
            return Ok(locations);
        }
    }
}

using System.Threading.Tasks;
using FlightMobileApp.Manager;
using FlightMobileApp.Model.Concrets_Objects;
using FlightMobileApp.Model.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightMobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly IConnectionManager manager;
        public CommandController(IConnectionManager manager)
        {
            this.manager = manager;
        }

        // POST: api/command
        [HttpPost]
        async public Task<int> SetValues([FromBody] Command command)
        {
            try
            {
                Result res = await manager.Execute(command);
                if (res == Result.Ok)
                {
                    return StatusCodes.Status200OK;
                }
                else
                {
                    return StatusCodes.Status400BadRequest;
                }
            }
            catch
            {
                return StatusCodes.Status400BadRequest;
            }
        }

    }
}

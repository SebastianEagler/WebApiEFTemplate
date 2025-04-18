using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WebApiEFTemplate.Database;

namespace WebApiEFTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        
        private readonly ILogger<PingController> _logger;
        private readonly DatabaseContext _databaseContext;

        public PingController(ILogger<PingController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpGet(Name = "Ping")]
        public IResult Get()
        {

            if (_databaseContext.Database.CanConnect())
            {
                _logger.LogDebug("Pong");
                return Results.Ok( "Pong" );
                
            }
            else
            {
                _logger.LogError("Can not connect to database!");
                return Results.Problem( detail:"Database connection failed.", title:"An error occurred while processing your request");
            }
        }
    }
}

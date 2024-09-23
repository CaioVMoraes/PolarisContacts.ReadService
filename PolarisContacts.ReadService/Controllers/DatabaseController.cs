using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController(ILogger<DatabaseController> logger) : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger = logger;

        [HttpPost]
        public bool Post()
        {
            try
            {
                //_databaseService.AddDatabase(new Domain.Database());
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CelularController(ILogger<CelularController> logger, ICelularService celularService) : ControllerBase
    {
        private readonly ILogger<CelularController> _logger = logger;
        private readonly ICelularService _celularService = celularService;

        [HttpPost]
        public bool Post()
        {
            try
            {
                _celularService.AddCelular(new Domain.Celular());
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}

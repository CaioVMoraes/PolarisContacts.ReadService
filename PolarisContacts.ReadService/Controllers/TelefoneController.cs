using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelefoneController(ILogger<TelefoneController> logger, ITelefoneService telefoneService) : ControllerBase
    {
        private readonly ILogger<TelefoneController> _logger = logger;
        private readonly ITelefoneService _telefoneService = telefoneService;

        [HttpPost]
        public bool Post()
        {
            try
            {
                _telefoneService.AddTelefone(new Domain.Telefone());
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}

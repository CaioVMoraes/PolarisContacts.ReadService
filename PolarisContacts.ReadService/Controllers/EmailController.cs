using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController(ILogger<EmailController> logger, IEmailService emailService) : ControllerBase
    {
        private readonly ILogger<EmailController> _logger = logger;
        private readonly IEmailService _emailService = emailService;

        [HttpPost]
        public bool Post()
        {
            try
            {
                _emailService.AddEmail(new Domain.Email());
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}

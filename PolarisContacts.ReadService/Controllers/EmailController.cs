using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController(ILogger<EmailController> logger, IEmailService emailService) : ControllerBase
    {
        private readonly ILogger<EmailController> _logger = logger;
        private readonly IEmailService _emailService = emailService;

        [HttpGet]
        public async Task<IEnumerable<Email>> GetEmailsByIdContato(int idContato)
        {
            try
            {
                return await _emailService.GetEmailsByIdContato(idContato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<Email> GetEmailById(int id)
        {
            try
            {
                return await _emailService.GetEmailById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

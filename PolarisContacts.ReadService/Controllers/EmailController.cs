using Microsoft.AspNetCore.Mvc;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.ReadService.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController(ILogger<EmailController> logger, IEmailService emailService) : ControllerBase
    {
        private readonly ILogger<EmailController> _logger = logger;
        private readonly IEmailService _emailService = emailService;

        [HttpGet("GetEmailsByIdContato/{idContato}")]
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

        [HttpGet("GetEmailById/{id}")]
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

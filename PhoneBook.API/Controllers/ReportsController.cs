using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PhoneBook.API.Constants;
using PhoneBook.API.Dto;
using RabbitMQ.Client;
using System.Text;

namespace PhoneBook.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PhoneBookSettings _phoneBookSettings;

        public ReportsController(IHttpClientFactory httpClientFactory, ILogger<ReportsController> logger, IOptions<PhoneBookSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _phoneBookSettings = options.Value;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<ActionResult> GenerateReport()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_phoneBookSettings.ReportApiUrl}/Raporlar/Talep");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return StatusCode(500);

            var responseStream = await response.Content.ReadAsStringAsync();

            var model = new ReportRequestDto()
            {
                ReportId = JsonConvert.DeserializeObject<Guid>(responseStream)
            };

            var conn = _phoneBookSettings.RabbitMqCon;

            var createDocumentQueue = "create_document_queue";
            var documentCreateExchange = "document_create_exchange";

            ConnectionFactory connectionFactory = new()
            {
                Uri = new Uri(conn)
            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare(documentCreateExchange, "direct");

            channel.QueueDeclare(createDocumentQueue, false, false, false);
            channel.QueueBind(createDocumentQueue, documentCreateExchange, createDocumentQueue);

            channel.BasicPublish(documentCreateExchange, createDocumentQueue, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)));

            return Accepted();
        }
    }
}

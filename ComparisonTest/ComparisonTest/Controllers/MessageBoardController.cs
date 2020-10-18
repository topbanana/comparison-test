using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComparisonTest.Models;
using ComparisonTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComparisonTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageBoardController : ControllerBase
    {
        private readonly ILogger<MessageBoardController> _logger;
        private readonly IMessageBoardService _messageBoardService;

        public MessageBoardController(IMessageBoardService messageBoardService, ILogger<MessageBoardController> logger)
        {
            _messageBoardService = messageBoardService ?? throw new ArgumentNullException(nameof(messageBoardService));
            _logger = logger;
        }

        [HttpGet]
        public IAsyncEnumerable<Message> Get()
        {
            return _messageBoardService.GetAllMessages();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Message message)
        {
            await _messageBoardService.AddMessage(message);
            return Ok();
        }
    }
}
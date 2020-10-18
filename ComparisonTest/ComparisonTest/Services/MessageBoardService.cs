using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComparisonTest.Models;
using Microsoft.Extensions.Logging;

namespace ComparisonTest.Services
{
    /// <inheritdoc />
    public class MessageBoardService : IMessageBoardService
    {
        private readonly ILogger<MessageBoardService> _logger;
        private readonly IRepository<MessageEntity> _messageRepository;

        public MessageBoardService(IRepository<MessageEntity> messageRepository,
            ILogger<MessageBoardService> logger)
        {
            _messageRepository =
                messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task AddMessage(Message message)
        {
            await _messageRepository.Add(new MessageEntity
            {
                Author = message.Author,
                Body = message.Body,
                DateTime = message.DateTime,
                ParentMessageId = message.ParentMessageId
            });
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<Message> GetAllMessages()
        {
            await foreach (var entity in _messageRepository.GetAll())
                yield return new Message
                {
                    Author = entity.Author,
                    Body = entity.Body,
                    DateTime = entity.DateTime,
                    ParentMessageId = entity.ParentMessageId
                };
            await Task.CompletedTask;
        }
    }
}
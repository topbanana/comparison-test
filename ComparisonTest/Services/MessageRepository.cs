using System.Collections.Generic;
using System.Threading.Tasks;
using ComparisonTest.Models;

namespace ComparisonTest.Services
{
    /// <summary>
    /// The is an implementation of <see cref="IRepository{MessageEntity}" /> that allows this to run but offers no
    /// persistence and should be swapped out to allow for a real backend
    /// </summary>
    public class MessageRepository : IRepository<MessageEntity>
    {
        private readonly List<MessageEntity> _messages;

        public MessageRepository()
        {
            _messages = new List<MessageEntity>();
        }

        /// <inheritdoc />
        public async Task Add(MessageEntity message)
        {
            _messages.Add(message);
            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<MessageEntity> GetAll()
        {
            foreach (var message in _messages) yield return message;
            await Task.CompletedTask;
        }
    }
}
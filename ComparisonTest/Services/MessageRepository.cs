using System.Collections.Generic;
using System.Threading.Tasks;
using ComparisonTest.Models;

namespace ComparisonTest.Services
{
    /// <summary>
    /// The is an implementation of <see cref="IRepository{MessageEntity}" /> that allows this application to run but
    /// offers no persistence and should be swapped out to allow for a persistent backend. This class is also not under
    /// test as I would not expect this implementation to go to production. This was added due to time constraints
    /// and I would expect to wrap an entity-framework data-context and not a list. This is also added as a singleton
    /// in the IOC container but List is not thread-safe. This is also present to ensure that models passed in are not
    /// entities and allow for decoration with data-annotations on the dto <see cref="Message" />.
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
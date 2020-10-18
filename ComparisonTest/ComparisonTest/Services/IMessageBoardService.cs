using System.Collections.Generic;
using System.Threading.Tasks;
using ComparisonTest.Models;

namespace ComparisonTest.Services
{
    /// <summary>
    /// Encapsulates operations on the message-board
    /// </summary>
    public interface IMessageBoardService
    {
        /// <summary>
        /// Adds a message to the message-board
        /// </summary>
        /// <param name="message">The message to add</param>
        /// <returns>An awaitable task</returns>
        Task AddMessage(Message message);

        /// <summary>
        /// Retrieves all messages on the message-board
        /// </summary>
        /// <returns>The messages held within storage</returns>
        IAsyncEnumerable<Message> GetAllMessages();
    }
}
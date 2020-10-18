using System.Collections.Generic;
using System.Threading.Tasks;
using ComparisonTest.Models;

namespace ComparisonTest.Services
{
    /// <summary>
    /// A limited repository interface to match test requirements
    /// </summary>
    /// <typeparam name="T">The entity to manage</typeparam>
    public interface IRepository<in T> where T : class
    {
        /// <summary>
        /// Adds an entity to storage
        /// </summary>
        /// <param name="entity">The entity to store</param>
        /// <returns>An awaitable task</returns>
        Task Add(T entity);

        /// <summary>
        /// Retrieves all stored entities
        /// </summary>
        /// <returns>The entities in storage</returns>
        IAsyncEnumerable<MessageEntity> GetAll();
    }
}
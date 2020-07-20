using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HFWEBAPI.DataAccess
{
    public interface IAppointmentRepository<T> where T : class
    { 
        Task<Document> CreateItemAsync(T item, string collectionId);
        Task DeleteItemAsync(string id, string collectionId, string partitionKey);
        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, string collectionId);
        //Task<IEnumerable<T>> GetUserAppointmentsAsync(string userId, string collectionId);
        //Task<IEnumerable<T>> GetAppointmentsByDateAsync(string start, string end, string collectionId);
        Task<Document> UpdateItemAsync(string id, T item, string collectionId);
    }
}

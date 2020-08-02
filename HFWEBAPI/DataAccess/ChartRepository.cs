using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HFWEBAPI.DataAccess
{
    public class ChartRepository<T> : IChartRepository<T> where T : class
    {
        private DocumentClient client;
        private IConfiguration _config;

        public ChartRepository(IConfiguration configuration)
        {
            _config = configuration;
            client = new DocumentClient(new Uri(_config.GetValue<string>("Values:CosmosDBEndpoint")), _config.GetValue<string>("Values:CosmosDBKey"));
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, string collectionId)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME"), collectionId),
                new FeedOptions { EnableCrossPartitionQuery = true, MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }
            return results;
        }


        public async Task<Document> CreateItemAsync(T item, string collectionId)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME"), collectionId), item);
        }

        public async Task<Document> UpdateItemAsync(string id, T item, string collectionId)
        {
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME"), collectionId, id), item);
        }

        public async Task DeleteItemAsync(string id, string collectionId, string partitionKey)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME"), collectionId, id),
            new RequestOptions() { PartitionKey = new PartitionKey(partitionKey) });
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME")));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = _config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME") });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync(string collectionId)
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME"), collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_config.GetValue<string>("Values:COSMOSDB_DATABASE_NAME")),
                        new DocumentCollection { Id = collectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recordbook_WebAPI.Models;
using Microsoft.Azure.Cosmos;

namespace Recordbook_WebAPI.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _recordsContainer, _speakersContainer, _tagsContainer;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string[] containerNames)
        {
            _recordsContainer = dbClient.GetContainer(databaseName, containerNames[0]);
            _speakersContainer = dbClient.GetContainer(databaseName, containerNames[1]);
            _tagsContainer = _recordsContainer;
        }


        public async Task<Record> GetRecordAsync(string id)
        {
            try
            {
                ItemResponse<Record> response = await _recordsContainer.ReadItemAsync<Record>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<Speaker> GetSpeakerAsync(string id)
        {
            try
            {
                ItemResponse<Speaker> response = await _speakersContainer.ReadItemAsync<Speaker>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<Tag> GetTagAsync(string id)
        {
            try
            {
                ItemResponse<Tag> response = await _tagsContainer.ReadItemAsync<Tag>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }


        public async Task<IEnumerable<Record>> GetRecordsAsync(string queryString)
        {
            var query = _recordsContainer.GetItemQueryIterator<Record>(new QueryDefinition(queryString));
            List<Record> results = new List<Record>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
        public async Task<IEnumerable<Speaker>> GetSpeakersAsync(string queryString)
        {
            var query = _speakersContainer.GetItemQueryIterator<Speaker>(new QueryDefinition(queryString));
            List<Speaker> results = new List<Speaker>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
        public async Task<IEnumerable<Tag>> GetTagsAsync(string queryString)
        {
            var query = _tagsContainer.GetItemQueryIterator<Tag>(new QueryDefinition(queryString));
            List<Tag> results = new List<Tag>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }


        public async Task AddRecordAsync(Record record)
        {
            record.Id = System.Guid.NewGuid().ToString();
            await _recordsContainer.CreateItemAsync(record);
        }


        public async Task DeleteRecordAsync(string id)
        {
            await _recordsContainer.DeleteItemAsync<Record>(id, new PartitionKey(id));
        }


        public async Task UpdateRecordAsync(string id, Record message)
        {
            await _recordsContainer.UpsertItemAsync(message, new PartitionKey(id));
        }
    }
}

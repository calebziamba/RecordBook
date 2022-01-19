using Recordbook_WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recordbook_WebAPI.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Record>> GetRecordsAsync(string query);
        Task<Record> GetRecordAsync(string id);
        Task AddRecordAsync(Record message);
        Task UpdateRecordAsync(string id, Record message);
        Task DeleteRecordAsync(string id);

        Task<IEnumerable<Speaker>> GetSpeakersAsync(string query);
        Task<Speaker> GetSpeakerAsync(string id);


        Task<IEnumerable<Tag>> GetTagsAsync(string query);
        Task<Tag> GetTagAsync(string value);
    }
}

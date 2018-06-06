using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Models;

namespace WeatherApp.Common.Services.Interfaces
{
    public interface IDataService<T> where T : BaseModel
    {
        Task Setup();
        Task InsertAllAsync(IEnumerable<T> items);
        Task<List<T>> GetAll();
        Task DeleteAll();
    }
}

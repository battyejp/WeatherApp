using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Models;

namespace WeatherApp.Common.Services.Interfaces
{
    public interface IDataService<T> where T : BaseModel
    {
        void Setup();
        void InsertAll(IEnumerable<T> items);
        List<T> GetAll();
        void DeleteAll();
        void RefreshData(IEnumerable<T> items);
    }
}

using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Models;
using WeatherApp.Common.Services.Interfaces;

namespace WeatherApp.Common.Services
{
    public class DataService<T> : IDataService<T> where T : BaseModel, new()
    {
        private SQLiteAsyncConnection database;

        public DataService(IFileService fileService)
        {
            database = new SQLiteAsyncConnection(fileService.GetLocalFilePath("ScannerSQLite.db3"));
        }

        public async Task Setup()
        {
            await database.CreateTableAsync<T>();
        }

        public async Task InsertAllAsync(IEnumerable<T> items)
        {
            await database.InsertAllAsync(items);
        }

        public async Task<List<T>> GetAll()
        {
            return await database.QueryAsync<T>($"Select * from { typeof(T).Name }");
        }

        public async Task DeleteAll()
        {
            await database.ExecuteAsync($"Delete from { typeof(T).Name }");
        }
    }
}
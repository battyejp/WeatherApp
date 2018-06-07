using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Models;
using WeatherApp.Common.Services.Interfaces;

namespace WeatherApp.Common.Services
{
    public class DataService<T> : IDataService<T> where T : BaseModel, new()
    {
        private SQLiteConnection database;

        public DataService(IFileService fileService)
        {
            database = new SQLiteConnection(fileService.GetLocalFilePath("ScannerSQLite.db3"));
        }

        public void Setup()
        {
            database.CreateTable<T>();
        }

        public void InsertAll(IEnumerable<T> items)
        {
            database.InsertAll(items);
        }

        public List<T> GetAll()
        {
            return database.Query<T>($"Select * from { typeof(T).Name }");
        }

        public void DeleteAll()
        {
            database.Execute($"Delete from { typeof(T).Name }");
        }

        public void RefreshData(IEnumerable<T> items)
        {
            DeleteAll();
            InsertAll(items);
        }
    }
}
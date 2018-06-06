using System;
using System.IO;

using WeatherApp.Common.Services.Interfaces;

namespace WeatherApp
{
    public class FileService : IFileService
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
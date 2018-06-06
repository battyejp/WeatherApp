using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Common.Services.Interfaces
{
    public interface IFileService
    {
        string GetLocalFilePath(string filename);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddLocationAsync(T item);
        Task<bool> UpdateLocationAsync(T item);
        Task<bool> DeleteLocationAsync(string id);
        Task<T> GetLocationAsync(string id);
        Task<IEnumerable<T>> GetLocationsAsync(bool forceRefresh = false);
    }
}

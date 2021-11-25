using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class MockDataStore : IDataStore<Location>
    {
        readonly List<Location> locations;

        public MockDataStore()
        {
            locations = new List<Location>()
            {
                new Location { Id = Guid.NewGuid().ToString(), City="Gliwice", Country="PL" },
                new Location { Id = Guid.NewGuid().ToString(), City="Warszawa", Country="PL" },
                new Location { Id = Guid.NewGuid().ToString(), City="London", Country="GB" },
                new Location { Id = Guid.NewGuid().ToString(), City="London", Country="CA" },
                new Location { Id = Guid.NewGuid().ToString(), City="Moskwa", Country="RU" },
                new Location { Id = Guid.NewGuid().ToString(), City="San Francisco", Country="US" },
                new Location { Id = Guid.NewGuid().ToString(), City="San Francisco", Country="CR" },
            };
        }

        public async Task<bool> AddLocationAsync(Location location)
        {
            locations.Add(location);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateLocationAsync(Location location)
        {
            var oldLocation = locations.Where((Location arg) => arg.Id == location.Id).FirstOrDefault();
            locations.Remove(oldLocation);
            locations.Add(location);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteLocationAsync(string id)
        {
            var oldLocation = locations.Where((Location arg) => arg.Id == id).FirstOrDefault();
            locations.Remove(oldLocation);

            return await Task.FromResult(true);
        }

        public async Task<Location> GetLocationAsync(string id)
        {
            return await Task.FromResult(locations.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Location>> GetLocationsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(locations);
        }
    }
}
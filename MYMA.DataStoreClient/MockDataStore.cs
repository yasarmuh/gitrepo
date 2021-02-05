using MYMA.DataStore.Client.Services;
using MYMA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMA.DataStore.Client.Services
{
    public class MockDataStore : IDataStore<Student>
    {
        readonly List<Student> items;

        public MockDataStore()
        {
            items = new List<Student>()
            {
                //new Student { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                //new Student { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                //new Student { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                //new Student { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                //new Student { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                //new Student { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Student item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            var oldItem = items.Where((arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Student> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Student>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }
    }
}
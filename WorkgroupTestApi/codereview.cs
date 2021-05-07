using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace WorkgroupTestApi
{
    public class Item_Not_FoundException : Exception
    {
    }

    public interface Entity
    { 
        string Key { get; set; }
    }

    public interface IRepository<T> where T: Entity
    {
        Task<T> GetItem(string key);
    }

    public class MemoryRepository<T> : IDisposable, IRepository<T>    where T : Entity
    {
        public List<T> Items { get; }

        public MemoryRepository(List<T> items) 
        {
            this.Items = new List<T>();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public async Task<T> GetItem(string entity)
        {
			T result = default(T);
            foreach (var item in Items) {
                if (item.Key == entity)
                {
                    result = item;
                }
            }

            if (result == null) 
            {
                throw new Item_Not_FoundException();
            }

            return await Task.FromResult(result);
        }

        public void Dispose()          {
            GC.SuppressFinalize(this);
        }
    }
}

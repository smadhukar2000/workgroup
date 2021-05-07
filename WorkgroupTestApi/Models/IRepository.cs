using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkgroupTestApi.Models
{
	public interface IRepository
	{
        // Returns a found key or null.
        string Get(string key);
        
        /// <summary>
        /// Create key value pair
        /// </summary>
        /// <param name="kv"></param>
        /// <returns>true if success, false if key already exist</returns>
        bool Create(KeyValue kv);

        /// <summary>
        /// Remove key value from repository
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(string key);
        
        /// <summary>
        /// Update the key value entry
        /// </summary>
        /// <param name="kv"></param>
        /// <returns>true on success, false if key not found</returns>
        bool Update(KeyValue kv);
    }
}

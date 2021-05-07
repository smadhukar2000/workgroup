using System;
using System.Collections.Generic;


namespace WorkgroupTestApi.Models
{
	public class Repository : IRepository
	{
		// Case insensitive dictionary key
		static Dictionary<string, string> repository = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
		public bool Create(KeyValue kv)
		{
			if (repository.ContainsKey(kv.Key))
			{
				return Update(kv);
			}
			repository.Add(kv.Key, kv.Value);
			return true;
		}

		public bool Delete(string key)
		{
			if (repository.ContainsKey(key))
			{
				return repository.Remove(key);
			}
			return false;
		}

		public string Get(string key)
		{
			if (repository.ContainsKey(key))
			{
				return repository[key];
			}
			return null;
		}

		public bool Update(KeyValue kv)
		{
			if (repository.ContainsKey(kv.Key))
			{
				repository[kv.Key] = kv.Value;
				return true;
			}
			return false;
		}
	}
}

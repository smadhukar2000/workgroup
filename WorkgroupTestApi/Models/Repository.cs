using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkgroupTestApi.Models
{
	public class Repository : IRepository
	{
		static Dictionary<string, string> repository = new Dictionary<string, string>();
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

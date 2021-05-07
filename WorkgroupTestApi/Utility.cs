using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkgroupTestApi
{
	public class Utility
	{
		const int MAX_KEY_LENGTH = 32;
		const int MAX_VALUE_LENGTH = 1024;

		public static bool ValidateKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key) || key.Length > MAX_KEY_LENGTH)
			{
				return false;
			}

			string pattern = @"^[a-zA-Z0-9_.\-~]*$";
			return Regex.IsMatch(key, pattern, RegexOptions.Singleline);
		}

		public static bool ValidateValue(string value)
		{
			if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_VALUE_LENGTH)
			{
				return false;
			}
			return true;
		}
	}
}

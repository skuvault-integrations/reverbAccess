using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Helpers
{
	public static class IdHelper
	{
		public static String GetUniqueId(String url)
		{
			String[] partsUrl = url.Split('/');
			String id = String.Empty;

			foreach (var item in partsUrl)
			{
				if (item.Length < 6) continue;
				String presumablyId = item.Substring(0, 6);

				Int32 intId = 0;
				if (Int32.TryParse(presumablyId, out intId))
				{
					return presumablyId;
				}
			}

			return "";
		}
	}
}

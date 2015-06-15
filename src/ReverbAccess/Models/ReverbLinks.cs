using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models
{
	public class ReverbLinks
	{
		public ReverbLinksItem next { get; set; }

		public ReverbLinksItem prev { get; set; }
	}

	public class ReverbLinksItem
	{
		public String href { get; set; }
	}
}

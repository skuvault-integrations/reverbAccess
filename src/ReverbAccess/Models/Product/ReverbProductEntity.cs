using ReverbAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models.Product
{
	public class ReverbProductEntity
	{
		public bool HasInventory { get; set; }

		public int Inventory { get; set; }

		public string Sku { get; set; }

		public string Slug { get; set; }

		public ReverbPrice Price { get; set; }
	}
}

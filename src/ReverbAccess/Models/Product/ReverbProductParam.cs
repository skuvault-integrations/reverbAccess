﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models.Product
{
	[DataContract]
	public class ReverbProductParam
	{
		[DataMember(Name = "has_inventory")]
		public bool HasInventory { get; set; }

		[DataMember(Name = "inventory")]
		public int Inventory { get; set; }

		[DataMember(Name = "sku")]
		public string Sku { get; set; }

		[DataMember(Name = "slug")]
		public string Slug { get; set; }
		[DataMember(Name = "price")]
		public ReverbPrice Price { get; set; }
	}
}

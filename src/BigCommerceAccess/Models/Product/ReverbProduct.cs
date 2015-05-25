using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReverbAccess.Models.Product
{
	[ DataContract ]
	public class ReverbProduct : ReverbProductBase
	{
		[ DataMember( Name = "inventory_tracking" ) ]
		public InventoryTrackingEnum InventoryTracking { get; set; }

		[ DataMember( Name = "skus" ) ]
		public ReverbReferenceObject ProductOptionsReference { get; set; }

		public List< ReverbProductOption > ProductOptions { get; set; }

		public ReverbProduct()
		{
			this.ProductOptions = new List< ReverbProductOption >();
		}
	}

	public enum InventoryTrackingEnum
	{
		none,
		simple,
		sku
	}
}
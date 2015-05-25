using System.Runtime.Serialization;

namespace ReverbAccess.Models.Product
{
	public class ReverbProductBase : ReverbObjectBase
	{
		[ DataMember( Name = "inventory_level" ) ]
		public string Quantity { get; set; }

		[ DataMember( Name = "sku" ) ]
		public string Sku { get; set; }
	}
}
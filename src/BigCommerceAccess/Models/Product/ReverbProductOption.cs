using System.Runtime.Serialization;

namespace ReverbAccess.Models.Product
{
	[ DataContract ]
	public class ReverbProductOption : ReverbProductBase
	{
		[ DataMember( Name = "product_id" ) ]
		public long ProductId { get; set; }
	}
}
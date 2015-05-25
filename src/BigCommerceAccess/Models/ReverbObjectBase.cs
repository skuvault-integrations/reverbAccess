using System.Runtime.Serialization;

namespace ReverbAccess.Models
{
	public abstract class ReverbObjectBase
	{
		[ DataMember( Name = "id" ) ]
		public long Id { get; set; }
	}
}
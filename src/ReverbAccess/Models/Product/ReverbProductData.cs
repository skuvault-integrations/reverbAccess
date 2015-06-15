using ReverbAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models.Product
{
	public class ReverbProductData
	{
		public String make { get; set; }

		public String model { get; set; }

		public String finish { get; set; }

		public String year { get; set; }

		public String title { get; set; }

		public String created_at { get; set; }

		public String shop_name { get; set; }

		public String description { get; set; }

		public String condition { get; set; }

		public ReverbPrice price { get; set; }

		public Boolean offers_enabled { get; set; }

		public Boolean has_offer_for_buyer { get; set; }

		public Boolean wanted { get; set; }

		public ReverbLocation location { get; set; }

		public Boolean has_inventory { get; set; }

		public Int32 inventory { get; set; }

		public Boolean handmade { get; set; }

		public Boolean draft { get; set; }

		public Boolean live { get; set; }

		public Boolean local_pickup_only { get; set; }

		public ReverbProductDataShop shop { get; set; }

		public ReverbProductDataStats stats { get; set; }

		public Int32 offer_count { get; set; }

		public String shipping_policy { get; set; }

		public Boolean is_my_Product { get; set; }

		public ReverbProductDataShipping shipping { get; set; }

		public ReverbProductItemLinks _links { get; set; }

		public String unique_id
		{
			get
			{
				var links = this._links;
				if (links.self != null && !String.IsNullOrEmpty(links.self.href))
				{
					return IdHelper.GetUniqueId(links.self.href);
				}
				else if (links.update != null && !String.IsNullOrEmpty(links.update.href))
				{
					return IdHelper.GetUniqueId(links.update.href);
				}
				else if (links.end != null && !String.IsNullOrEmpty(links.end.href))
				{
					return IdHelper.GetUniqueId(links.end.href);
				}
				else if (links.want != null && !String.IsNullOrEmpty(links.want.href))
				{
					return IdHelper.GetUniqueId(links.want.href);
				}
				else if (links.unwant != null && !String.IsNullOrEmpty(links.unwant.href))
				{
					return IdHelper.GetUniqueId(links.unwant.href);
				}
				else if (links.edit != null && !String.IsNullOrEmpty(links.edit.href))
				{
					return IdHelper.GetUniqueId(links.edit.href);
				}
				else if (links.web != null && !String.IsNullOrEmpty(links.web.href))
				{
					return IdHelper.GetUniqueId(links.web.href);
				}
				else if (links.add_to_wishlist != null && !String.IsNullOrEmpty(links.add_to_wishlist.href))
				{
					return IdHelper.GetUniqueId(links.add_to_wishlist.href);
				}
				else if (links.remove_from_wishlist != null && !String.IsNullOrEmpty(links.remove_from_wishlist.href))
				{
					return IdHelper.GetUniqueId(links.remove_from_wishlist.href);
				}
				else
				{
					return String.Empty;
				}
			}
		}
	}

	public class ReverbProductDataShop
	{
		public Int32 feedback_count { get; set; }

		public Boolean preferred_seller { get; set; }

		public Int32 rating_percentage { get; set; }
	}

	public class ReverbProductDataStats
	{
		public Int32 views { get; set; }

		public Int32 watches { get; set; }
	}

	public class ReverbProductDataShipping
	{
		public Boolean local { get; set; }

		public Boolean us { get; set; }

		public ReverbPrice us_rate { get; set; }
	}
}

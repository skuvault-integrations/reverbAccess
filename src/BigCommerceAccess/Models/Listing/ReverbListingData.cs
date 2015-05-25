using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models.Listing
{
    public class ReverbListingData
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

        public Boolean handmade { get; set; }

        public Boolean draft { get; set; }

        public Boolean live { get; set; }

        public Boolean local_pickup_only { get; set; }

        public ReverbListingDataShop shop { get; set; }

        public ReverbListingDataStats stats { get; set; }

        public Int32 offer_count { get; set; }

        public String shipping_policy { get; set; }

        public Boolean is_my_listing { get; set; }

        public ReverbListingDataShipping shipping { get; set; }
    }

    public class ReverbListingDataShop
    {
        public Int32 feedback_count { get; set; }

        public Boolean preferred_seller { get; set; }

        public Int32 rating_percentage { get; set; }
    }

    public class ReverbListingDataStats
    {
        public Int32 views { get; set; }

        public Int32 watches { get; set; }
    }

    public class ReverbListingDataShipping
    {
        public Boolean local { get; set; }

        public Boolean us { get; set; }

        public ReverbPrice us_rate { get; set; }
    }
}

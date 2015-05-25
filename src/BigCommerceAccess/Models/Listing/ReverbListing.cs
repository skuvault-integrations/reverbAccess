using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models.Listing
{
    public class ReverbListing
    {
        public List<ReverbListingItem> listings { get; set; }
    }

    public class ReverbListingItem
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

        public String etag { get; set; }

        public Boolean offers_enabled { get; set; }

        public Boolean bumped { get; set; }

        public ReverbListingItemLinks _links { get; set; }

        public ReverbListingItemShipping shipping { get; set; }

        public String sku { get; set; }
    }

    public class ReverbListingItemShipping
    {
        public Boolean local { get; set; }

        public Boolean us { get; set; }

        public ReverbPrice us_rate { get; set; }
    }

    public class ReverbListingItemLinks
    {
        public ReverbLink photo { get; set; }

        public ReverbLink self { get; set; }

        public ReverbLink update { get; set; }

        public ReverbLink end { get; set; }

        public ReverbLink want { get; set; }

        public ReverbLink unwant { get; set; }

        public ReverbLink edit { get; set; }

        public ReverbLink web { get; set; }

        public ReverbLink make_offer { get; set; }

        public ReverbLink add_to_wishlist { get; set; }

        public ReverbLink remove_from_wishlist { get; set; }
    }
}

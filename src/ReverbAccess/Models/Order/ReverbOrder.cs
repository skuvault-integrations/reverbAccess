using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ReverbAccess.Models.Address;

namespace ReverbAccess.Models.Order
{
    public class ReverbOrder
	{
        public ReverbLinks _links { get; set; }

        public List<ReverbOrderItem> orders { get; set; }
	}

    public class ReverbOrderItem
    {
        public ReverbPrice amount_product { get; set; }

        public ReverbPrice amount_product_subtotal { get; set; }

        public ReverbPrice shipping { get; set; }

        public ReverbPrice amount_tax { get; set; }

        public ReverbPrice total { get; set; }

        public String buyer_name { get; set; }

        public String created_at { get; set; }

        public String order_number { get; set; }

        public Boolean needs_feedback_for_buyer { get; set; }

        public Boolean needs_feedback_for_seller { get; set; }

        public String order_type { get; set; }

        public String paid_at { get; set; }

        public Int32 quantity { get; set; }

        public ReverbOrderItemShippingAddress shipping_address { get; set; }

        public Boolean local_pickup { get; set; }

        public String shop_name { get; set; }

        public String status { get; set; }

        public String title { get; set; }

        public String updated_at { get; set; }

        public String payment_method { get; set; }

        public String shipping_provider { get; set; }

        public String sku { get; set; }
    }

    public class ReverbOrderItemShippingAddress
    {
        public String name { get; set; }

        public String street_address { get; set; }

        public String extended_address { get; set; }

        public String locality { get; set; }

        public String region { get; set; }

        public String postal_code { get; set; }

        public String country_code { get; set; }

        public String phone { get; set; }
    }
}
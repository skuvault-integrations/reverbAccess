using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ReverbAccess.Models.Address;

namespace ReverbAccess.Models.Order
{
	public class ReverbOrderEntity
	{
		public ReverbOrderEntity()
		{
		}

		public ReverbOrderEntity(ReverbOrderItem obj)
		{
			this.AmountProduct = obj.amount_product;
			this.AmountProductSubtotal = obj.amount_product_subtotal;
			this.Shipping = obj.shipping;
			this.AmountTax = obj.amount_tax;
			this.Total = obj.total;
			this.BuyerName = obj.buyer_name;
			this.CreatedAt = obj.created_at;
			this.OrderNumber = obj.order_number;
			this.NeedsFeedbackForBuyer = obj.needs_feedback_for_buyer;
			this.NeedsFeedbackForSeller = obj.needs_feedback_for_seller;
			this.OrderType = obj.order_type;
			this.PaidAt = obj.paid_at;
			this.Quantity = obj.quantity;
			this.LocalPickup = obj.local_pickup;
			this.ShopName = obj.shop_name;
			this.StatusStr = obj.status;
			this.Title = obj.title;
			this.UpdatedAt = obj.updated_at;
			this.PaymentMethod = obj.payment_method;
			this.ShippingAddress = obj.shipping_address;
			this.ShippingProvider = obj.shipping_provider;
			this.ShippingCode = obj.shipping_code;
			this.Sku = obj.sku;
		}

		public ReverbPrice AmountProduct { get; set; }

		public ReverbPrice AmountProductSubtotal { get; set; }

		public ReverbPrice Shipping { get; set; }

		public ReverbPrice AmountTax { get; set; }

		public ReverbPrice Total { get; set; }

		public String BuyerName { get; set; }

		internal String CreatedAt { get; set; }

		public DateTime CreatedAtDate
		{
			get { return DateTime.SpecifyKind(Convert.ToDateTime(this.CreatedAt), DateTimeKind.Utc); }
		}

		public String OrderNumber { get; set; }

		public Boolean NeedsFeedbackForBuyer { get; set; }

		public Boolean NeedsFeedbackForSeller { get; set; }

		public String OrderType { get; set; }

		internal String PaidAt { get; set; }

		public DateTime PaidAtDate
		{
			get { return DateTime.SpecifyKind(Convert.ToDateTime(this.PaidAt), DateTimeKind.Utc); }
		}

		public Int32 Quantity { get; set; }

		public Boolean LocalPickup { get; set; }

		public String ShopName { get; set; }

		internal String StatusStr { get; set; }

		public ReverbOrderStatusEnum Status
		{
			get
			{
				switch (this.StatusStr)
				{
					case "unpaid":
						return ReverbOrderStatusEnum.Unpaid;
					case "payment_pending":
						return ReverbOrderStatusEnum.PaymentPending;
					case "paid":
						return ReverbOrderStatusEnum.Paid;
					case "shipped":
						return ReverbOrderStatusEnum.Shipped;
					case "picked_up":
						return ReverbOrderStatusEnum.PickedUp;
					case "received":
						return ReverbOrderStatusEnum.Received;
					case "presumed_received":
						return ReverbOrderStatusEnum.PresumedReceived;
					case "cancelled":
						return ReverbOrderStatusEnum.Cancelled;
					case "refunded":
						return ReverbOrderStatusEnum.Refunded;
					default:
						return ReverbOrderStatusEnum.Default;
				}
			}
		}

		public String Title { get; set; }

		internal String UpdatedAt { get; set; }

		public DateTime UpdatedAtDate
		{
			get { return DateTime.SpecifyKind(Convert.ToDateTime(this.UpdatedAt), DateTimeKind.Utc); }
		}

		public String PaymentMethod { get; set; }

		public ReverbOrderItemShippingAddress ShippingAddress { get; set; }

		public String ShippingProvider { get; set; }

		public String ShippingCode { get; set; }

		public String Sku { get; set; }
	}
}
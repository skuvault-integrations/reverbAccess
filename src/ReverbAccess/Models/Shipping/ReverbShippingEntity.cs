using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models.Shipping {
    public class ReverbShippingEntity {
        public String provider { get; set; }
        public String tracking_number { get; set; }
        public bool send_notification { get; set; }
    }
}

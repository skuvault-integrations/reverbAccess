using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverbAccess.Models.Product
{
    public class ReverbProductItemLinks
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

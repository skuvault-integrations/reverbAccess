using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Listing;

namespace ReverbAccess
{
	public interface IReverbListingsService
	{
		ReverbListing GetListings( DateTime dateFrom, DateTime dateTo );
        Task<ReverbListing> GetListingsAsync(DateTime dateFrom, DateTime dateTo);

        ReverbListing GetListingsDrafts(DateTime dateFrom, DateTime dateTo);
        Task<ReverbListing> GetListingsDraftsAsync(DateTime dateFrom, DateTime dateTo);

        ReverbListingData GetListingsById(String id);
        Task<ReverbListingData> GetListingsByIdAsync(String id);

        void UpdateListings(IEnumerable<ReverbListingParam> listings);
        Task UpdateListingsAsync(IEnumerable<ReverbListingParam> listings);
	}
}
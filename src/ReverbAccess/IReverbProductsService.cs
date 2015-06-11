using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Product;

namespace ReverbAccess
{
	public interface IReverbProductsService
	{
        IEnumerable<ReverbProductEntity> GetProducts(String state, Int32? page = null, Int32? per_page = null);
        Task<IEnumerable<ReverbProductEntity>> GetProductsAsync(String state, Int32? page = null, Int32? per_page = null);

        //ReverbProduct GetProductsDrafts(DateTime dateFrom, DateTime dateTo);
        //Task<ReverbProduct> GetProductsDraftsAsync(DateTime dateFrom, DateTime dateTo);

        //ReverbProductData GetProductsById(String id);
        //Task<ReverbProductData> GetProductsByIdAsync(String id);

        void UpdateProducts(IEnumerable<ReverbProductEntity> products);
        Task UpdateProductsAsync(IEnumerable<ReverbProductEntity> products);
	}
}
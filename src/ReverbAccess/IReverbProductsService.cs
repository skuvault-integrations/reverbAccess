using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Product;

namespace ReverbAccess
{
	public interface IReverbProductsService
	{
		IEnumerable<ReverbProductEntity> GetProducts();
		Task<IEnumerable<ReverbProductEntity>> GetProductsAsync();

		void UpdateProducts(IEnumerable<ReverbProductEntity> products);
		Task UpdateProductsAsync(IEnumerable<ReverbProductEntity> products);

		Boolean IsProductsReceived();
		Task<Boolean> IsProductsReceivedAsync();
	}
}
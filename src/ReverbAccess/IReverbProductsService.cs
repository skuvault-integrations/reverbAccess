using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Product;

namespace ReverbAccess
{
	public interface IReverbProductsService
	{
		IEnumerable<ReverbProductEntity> GetProducts(String state);
		Task<IEnumerable<ReverbProductEntity>> GetProductsAsync(String state);

		void UpdateProducts(IEnumerable<ReverbProductEntity> products);
		Task UpdateProductsAsync(IEnumerable<ReverbProductEntity> products);

		Boolean IsProductsReceived();
		Task<Boolean> IsProductsReceivedAsync();
	}
}
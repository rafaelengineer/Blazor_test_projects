using EinkaufOnline.Models.Dtos;

namespace EinkaufOnline.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
    }
}

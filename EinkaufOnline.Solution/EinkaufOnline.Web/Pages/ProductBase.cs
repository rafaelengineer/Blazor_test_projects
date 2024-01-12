using EinkaufOnline.Models.Dtos;
using EinkaufOnline.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace EinkaufOnline.Web.Pages
{
    public class ProductBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetItems();
        }
    }
}

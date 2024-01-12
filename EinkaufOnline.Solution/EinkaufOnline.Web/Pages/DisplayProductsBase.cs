using EinkaufOnline.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace EinkaufOnline.Web.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }


    }
}

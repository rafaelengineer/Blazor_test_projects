using EinkaufOnline.Models.Dtos;
using EinkaufOnline.Web.Services.Contracts;
using System.Data;
using System.Net.Http.Json;

namespace EinkaufOnline.Web.Services
{
    public class ProductService: IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Product");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }
                    return await this.httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                } 
            }
            catch (Exception ex)
            {
                //Log Exception.
                throw;
            }
        }
        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Product/{id}");
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent) 
                    {
                        return default(ProductDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}

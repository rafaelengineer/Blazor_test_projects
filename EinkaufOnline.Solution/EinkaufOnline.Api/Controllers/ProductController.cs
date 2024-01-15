using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EinkaufOnline.Api.Extensions;
using EinkaufOnline.Api.Repositories.Contracts;
using EinkaufOnline.Models.Dtos;

namespace EinkaufOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {

            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await this.productRepository.GetItems();
                var productcategories = await this.productRepository.GetCategories();


                if (products == null || productcategories == null)
                { 
                   return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto(productcategories);

                    return Ok(productDtos);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + Environment.NewLine +
                                ex.Message);
               
            }
        }
        //[HttpGet]
        //[Route("{userId}/GetItems")]
        //public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        //{
        //    try
        //    {
        //        var products = await this.productRepository.GetItems();


        //        if (products == null)
        //        { 
        //           return NotFound();
        //        }
        //        else
        //        {
        //            var productDtos = products.ConvertToDto();

        //            return Ok(productDtos);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database" + Environment.NewLine +
        //                        ex.Message);
               
        //    }
        //}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await this.productRepository.GetItem(id);
               
                if (product == null)
                {
                    return BadRequest();
                }
                else
                {
                    //var productCategory = await this.productRepository.GetCategory(id);
                    var productDto = product.ConvertToDto();
                    return  Ok(productDto);
                    //var productDto = product.ConvertToDto();
                    //return Ok(productDto);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await productRepository.GetCategories();
                
                var productCategoryDtos = productCategories.ConvertToDto();

                return Ok(productCategoryDtos);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

        }

        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var products = await productRepository.GetItemsByCategory(categoryId);

                var productDtos = products.ConvertToDto();

                return Ok(productDtos);
            
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

    }
}

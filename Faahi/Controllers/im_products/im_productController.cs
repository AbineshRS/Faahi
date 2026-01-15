using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.Product_dto;
using Faahi.Model.im_products;
using Faahi.Service;
using Faahi.Service.im_products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Faahi.Controllers.im_products
{
    [Route("api/[controller]")]
    [ApiController]
    public class im_productController : ControllerBase
    {
        private readonly Iim_products _im_products;
        private readonly ApplicationDbContext _context;
        public im_productController(Iim_products im_products, ApplicationDbContext context)
        {
            _im_products = im_products;
            _context = context;
        }
        [Authorize]
        [HttpPost]
        [Route("add_product")]
        public async Task<ActionResult<im_Products>> Create_prodcust(im_Products im_Product)
        {
            if (im_Product == null)
            {
                return Ok("No data found");
            }
            var im_prodcts = await _im_products.Create_Product(im_Product);

            return Ok(im_prodcts);
        }
        [Authorize]
        [HttpPost]
        [Route("UploadProductDefaultImage/{product_id}")]
        public async Task<ActionResult<string>> UploadProductAsync(IFormFile formFile, Guid product_id)
        {
            if (formFile == null || product_id == null)
            {
                return Ok("No data found");
            }
            var thumbnail = await _im_products.UploadProductAsync(formFile, product_id);
            return Ok(thumbnail);
        }
        //[Authorize]
        [HttpPost]
        [Route("UploadProductMultipleFiles/{product_id}/{variant_id}")]
        public async Task<ActionResult<string>> UploadMutiple_image(IFormFile[] formFiles, string product_id, string variant_id)
        {
            if (formFiles == null || product_id == null)
            {
                return Ok("no data found");
            }
            var uploadmutiple = await _im_products.UploadMutiple_image(formFiles, product_id, variant_id);
            return Ok(uploadmutiple);
        }
        [Authorize]
        [HttpPost]
        [Route("Delete_ProductImageForm")]
        public async Task<IActionResult> Delete_ProductImage([FromBody] DeleteImageDto deleteImageDto)
        {
            //var deleteDto = new DeleteImageDto
            //{
            //    Product_Id = productId,
            //    Variant_Id = variantId,
            //    Deleted_Images = deletedImages
            //};

            var result = await _im_products.Delete_ProductImage(deleteImageDto);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("Upload_vedio/{product_id}/{variant_id}")]
        public async Task<ActionResult<string>> Upload_vedios(IFormFile[] formFiles, string product_id, string variant_id)
        {
            if (formFiles == null || product_id == null)
            {
                return Ok("no data found");
            }
            var uploadmutiple = await _im_products.Upload_vedio(formFiles, product_id, variant_id);
            return Ok(uploadmutiple);
        }
        [Authorize]
        [HttpGet]
        [Route("get_company_product/{company_id}")]
        public async Task<IActionResult> get_company_product(string company_id)
        {
            if(company_id == null)
            {
                return Ok("NO id found");
            }
            var all_product_details = await _im_products.get_company_product(company_id);
            return Ok(all_product_details);
        }
        //[Authorize]
        [HttpGet]
        [Route("get_all_product_details/{company_id}")]
        public async Task<IActionResult> all_product_details(Guid company_id)
        {
            if (company_id == null)
            {
                return Ok("no data found");
            }
            var all_product_details = await _im_products.all_product_details(company_id);
            return Ok(all_product_details);
        }
        [Authorize]
        [HttpGet]
        [Route("get_Product_details/{product_id}")]
        public async Task<IActionResult> Get_product_details(Guid product_id)
        {
            if (product_id == null)
            {
                return Ok("no id found");
            }
            var product = await _im_products.Get_product_details(product_id);
            return Ok(product);
        }
        [Authorize]
        [HttpPost]
        [Route("Update_product/{product_id}")]
        public async Task<ActionResult<im_Products>> Update_Product(Guid product_id,im_Products im_Products)
        {
            if(product_id == null)
            {
                return Ok("Product Id not found");
            }
            var update_product = await _im_products.Update_Product(product_id,im_Products);
            return Ok(update_product);
        }
        [Authorize]
        [HttpPost]
        [Route("update_mutiple_product/{product_id}")]
        public async Task<ActionResult<im_Products>> Update_Mutiple_Product(Guid product_id, im_Products im_Products)
        {
            if (product_id == null)
            {
                return Ok("Product Id not found");
            }
            var update_product = await _im_products.Update_Mutiple_Product(product_id, im_Products);
            return Ok(update_product);
        }
        [Authorize]
        [HttpPost]
        [Route("add_subCategory/{product_id}")]
        public async Task<ActionResult<im_ProductVariants>> Add_SubCategory(string product_id,im_ProductVariants im_ProductVariants)
        {
            if (product_id == null)
            {
                return Ok("Product Id Not found");
            }
            var sub_category =await _im_products.Add_subCategory(product_id, im_ProductVariants);
            return Ok(sub_category);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete_product/{product_id}")]
        public async Task<ActionResult<im_Products>> Delete_product(string product_id)
        {
            if (product_id == null)
            {
                return Ok("No prodct id found");
            }
            var deleted_data = await _im_products.Delete_product(product_id);
            return Ok(deleted_data);

        }
        [Authorize]
        [HttpPost]
        [Route("add_attribute")]
        public async Task<ActionResult<im_ProductAttributes>> Create_Attribute(im_ProductAttributes im_ProductAttributes)
        {
            if(im_ProductAttributes == null)
            {
                return Ok("no data found");
            }
            var result = await _im_products.Create_Attribute(im_ProductAttributes);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_attribute/{company_id}")]
        public async Task<IActionResult> Get_attribute(Guid company_id)
        {
            if (company_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_products.Get_attribute(company_id);
            return Ok(result);
        }


        
        [HttpPost("encript/{id}")]
        public IActionResult EncryptId(string id)
        {
            var encrypted = EncryptionHelper.EncryptString(id); 
            var urlSafeEncrypted = Uri.EscapeDataString(encrypted); 
            return Ok(urlSafeEncrypted);
        }


        [HttpGet("get-by-id/{encryptedId}")]
        public IActionResult GetProductByEncryptedId(string encryptedId)
        {
            try
            {
                var base64 = Uri.UnescapeDataString(encryptedId);

                var decryptedId = EncryptionHelper.DecryptString(base64);

                return Ok(decryptedId);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid encrypted ID: {ex.Message}");
            }
        }
        //[HttpGet("update-by-id/{encryptedId}")]
        //public async Task<IActionResult> UpdateProductByEncryptedId(string encryptedId)
        //{
        //    try
        //    {
        //        // 1. Decode & decrypt the ID
        //        var base64 = Uri.UnescapeDataString(encryptedId);
        //        var productId = EncryptionHelper.DecryptString(base64);

        //        // 2. Find the product in DB
        //        var product = await _context.im_Products.FirstOrDefaultAsync(p => p.product_id == productId);
        //        if (product == null)
        //            return NotFound("Product not found");

        //        // 3. Update fields from payload

        //        var jsonData = JsonConvert.SerializeObject(product);
        //        var encryptedData = EncryptionHelper.EncryptString(jsonData);
        //        //await _context.SaveChangesAsync();

        //        return Ok(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}


    }
}

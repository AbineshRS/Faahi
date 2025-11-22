using Faahi.Dto;
using Faahi.Dto.Product_dto;
using Faahi.Model.im_products;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Service.im_products
{
    public interface Iim_products
    {

        Task<ServiceResult<im_Products>> Create_Product(im_Products im_Product);

        Task<ActionResult<ServiceResult<string>>> UploadProductAsync(IFormFile formFile,string product_id);

        Task<ActionResult<ServiceResult<string>>> UploadMutiple_image(IFormFile[] formFile,string product_id,string variant_id);
        
        Task<ActionResult<ServiceResult<string>>> Upload_vedio(IFormFile[] formFile,string product_id,string variant_id);
       
        Task<ServiceResult<string>> get_company_product(string company_id);

        Task<ServiceResult<im_Products>> all_product_details(Guid company_id);

        Task<ActionResult<ServiceResult<im_products_dto>>> Get_product_details(string product_id);

        Task<ActionResult<ServiceResult<im_Products>>> Update_Product(string product_id,im_Products im_Products);

        Task<ActionResult<ServiceResult<im_ProductVariants>>> Add_subCategory(string product_id,im_ProductVariants im_ProductVariants);

        Task<ActionResult<ServiceResult<im_Products>>> Delete_product(string product_id);

        Task<ServiceResult<im_ProductAttributes>> Create_Attribute(im_ProductAttributes im_ProductAttributes);

        Task<ServiceResult<List<im_ProductAttributes>>> Get_attribute(Guid company_id);
    }
}

using Faahi.Dto;
using Faahi.Dto.Product_dto;
using Faahi.Model.im_products;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Service.im_products
{
    public interface Iim_products
    {

        Task<ServiceResult<im_Products>> Create_Product(im_Products im_Product);

        Task<ServiceResult<List<im_ProductVariants>>> Add_varient(List<im_ProductVariants> im_ProductVariants,Guid product_id);

        Task<ActionResult<ServiceResult<string>>> UploadProductAsync(IFormFile formFile,Guid product_id);

        Task<ActionResult<ServiceResult<string>>> UploadMutiple_image(IFormFile[] formFile,string product_id,string variant_id);

        Task<ServiceResult<DeleteImageDto>> Delete_ProductImage(DeleteImageDto deleteImageDto);


        Task<ActionResult<ServiceResult<string>>> Upload_vedio(IFormFile[] formFile,string product_id,string variant_id);
       
        Task<ServiceResult<List<im_Products>>> get_company_product(Guid company_id,string searchText,Guid? categoryId);

        Task<ServiceResult<List<im_Products>>> all_product_details(Guid company_id);

        Task<ServiceResult<im_Products>> Get_product_details(Guid product_id);

        Task<ActionResult<ServiceResult<im_Products>>> Update_Product(Guid product_id,im_Products im_Products);

        Task<ServiceResult<im_Products>> Update_Mutiple_Product(Guid product_id, im_Products im_Products);

        Task<ActionResult<ServiceResult<im_ProductVariants>>> Add_subCategory(string product_id,im_ProductVariants im_ProductVariants);

        Task<ServiceResult<List<im_Products>>> Get_product_list();

        Task<ServiceResult<List<im_Products>>> Product_search(string search_text,Guid store_id);

        Task<ActionResult<ServiceResult<im_Products>>> Delete_product(string product_id);

        Task<ServiceResult<im_ProductAttributes>> Create_Attribute(im_ProductAttributes im_ProductAttributes);

        Task<ServiceResult<List<im_ProductAttributes>>> Get_attribute(Guid company_id);

        Task<ServiceResult<im_Products>> barcode_exist(string barcode,Guid store_id);

        Task<ServiceResult<im_product>> product_transfer_store(Guid product_id,Guid store_id);

        Task<ServiceResult<im_Products>> Delete_product_data(Guid product_id);
    }
}

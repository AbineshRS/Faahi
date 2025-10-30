using Faahi.Dto;
using Faahi.Model.im_products;

namespace Faahi.Service.im_products.category
{
    public interface ICategory
    {
        Task<ServiceResult<im_item_Category>> Create_category(im_item_Category category);

        Task<ServiceResult<im_item_subcategory>> Create_sub_category(im_item_subcategory im_Item_Subcategory, string item_class_id);

        Task<ServiceResult<List<im_item_Category>>> categoryList();

        Task<ServiceResult<im_item_Category>> category_list_id(string item_class_id);

        Task<ServiceResult<im_item_Category>> Update(im_item_Category category,string item_class_id);

        Task<ServiceResult<im_item_Category>> Delete(string item_class_id);

        ///From im_ProductCategories Tables
        ///
        Task<ServiceResult<im_ProductCategories>> Create_product_category(im_ProductCategories im_ProductCategories);

        Task<ServiceResult<List<im_ProductCategories>>> Get_all_product_category();

    }
}

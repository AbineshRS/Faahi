using Faahi.Dto;
using Faahi.Model.im_products;

namespace Faahi.Service.im_products.im_tags
{
    public interface Iim_tags
    {


        Task<ServiceResult<im_products_tag>> Create_tagsAsync(im_products_tag im_Products_Tag);

        Task<ServiceResult<List<im_products_tag>>> Tag_List();

        Task<ServiceResult<im_products_tag>> Tag_id(string tag_id);

        Task<ServiceResult<im_products_tag>> Update(im_products_tag im_Products_Tag,string tag_id);

        Task<ServiceResult<im_products_tag>> Delete(string tag_id);

        Task<ServiceResult<im_UnitsOfMeasure>> Create_umoAsync(im_UnitsOfMeasure im_Products_Tag);

        Task<ServiceResult<List<im_UnitsOfMeasure>>> Uom_List();

        Task<ServiceResult<im_UnitsOfMeasure>> uom_id(string uom_id);

        Task<ServiceResult<im_UnitsOfMeasure>> Update_uom(im_UnitsOfMeasure im_UnitsOfMeasure, string uom_id);

        Task<ServiceResult<List<im_UnitsOfMeasure>>> umo_list();

        Task<ServiceResult<im_UnitsOfMeasure>> Delete_umo(string tag_id);






    }
}

using Faahi.Model.im_products;
using Faahi.Model.Stores;
using Faahi.Service.im_products.category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.im_products.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }

        [Authorize]
        [HttpPost]
        [Route("add_category")]
        public async Task<ActionResult<im_item_Category>> Create_category(im_item_Category im_Item_Category)
        {
            if (im_Item_Category == null)
            {
                return Ok("no data found");
            }
            var Category = await _category.Create_category(im_Item_Category);
            return Ok(Category);
        }
        [Authorize]
        [HttpPost]
        [Route("add_sub_category/{item_class_id}")]
        public async Task<ActionResult<im_item_subcategory>> Create_sub_category(im_item_subcategory im_Item_Subcategory, string item_class_id)
        {
            if (im_Item_Subcategory == null)
            {
                return Ok("No data found");
            }
            var sub_category = await _category.Create_sub_category(im_Item_Subcategory, item_class_id);
            return Ok(sub_category);
        }
        [Authorize]
        [HttpGet]
        [Route("category_list")]
        public async Task<IActionResult> category_list()
        {
            var Category = await _category.categoryList();
            return Ok(Category);
        }
        [Authorize]
        [HttpGet]
        [Route("category_list/{item_class_id}")]
        public async Task<IActionResult> category_list(string item_class_id)
        {
            var Category = await _category.category_list_id(item_class_id);
            return Ok(Category);
        }
        [Authorize]
        [HttpPost]
        [Route("update/{item_class_id}")]
        public async Task<ActionResult<im_item_Category>> Update(im_item_Category im_Item_, string item_class_id)
        {
            if (im_Item_ == null)
            {
                return Ok("no data found");
            }
            var update_category = await _category.Update(im_Item_, item_class_id);
            return Ok(update_category);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete/{item_class_id}")]
        public async Task<IActionResult> Delete(string item_class_id)
        {
            if (item_class_id == null)
            {
                return Ok("no data found");
            }
            var deleted_category = await _category.Delete(item_class_id);
            return Ok(deleted_category);
        }

        ///From im_ProductCategories Tables
        //[Authorize]
        [HttpPost]
        [Route("add_product_category")]
        public async Task<ActionResult<im_ProductCategories>> Add_product_category(im_ProductCategories im_ProductCategories)
        {
            if (im_ProductCategories == null)
            {
                return Ok("no data found");
            }
            var created = await _category.Create_product_category(im_ProductCategories);
            return Ok(created);
        }
        [HttpGet]
        [Route("product_category_list")]
        public async Task<IActionResult> product_category_list()
        {
            var result = await _category.Get_all_product_category();
            return Ok(result);
        }
        //[Authorize]
        [HttpPost]
        [Route("add_StoreCategories")]
        public async Task<ActionResult<List<st_StoreCategories>>> Add_StoreCategories(List<st_StoreCategories> im_StoreCategories)
        {
            if (im_StoreCategories == null)
            {
                return Ok("no data found");
            }
            var created = await _category.Create_StoreCategories(im_StoreCategories);
            return Ok(created);
        }
    }
}

using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.im_products.category
{
    public class CategoryService:ICategory
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<im_item_Category>> Create_category(im_item_Category im_Item_)
        {
            if (im_Item_ == null)
            {
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "NO data found to insert",
                    Status = -1,

                };
            }
          

            im_Item_.item_class_id = Guid.CreateVersion7();
            im_Item_.company_id = im_Item_.company_id;
            im_Item_.item_class = im_Item_.item_class;
            im_Item_.description = im_Item_.description;
            im_Item_.categoryType = im_Item_.categoryType;
            im_Item_.item_count = im_Item_.item_count;
            im_Item_.Sales_count = im_Item_.Sales_count;
            im_Item_.code = im_Item_.item_class.Substring(0, 3).ToUpper();
            im_Item_.edit_user_id = im_Item_.edit_user_id;
            im_Item_.edit_date_time = DateTime.Now;
            im_Item_.categoryImage = im_Item_.categoryImage;
            im_Item_.status = im_Item_.status;
            im_Item_.publish = im_Item_.publish;
            foreach (var im_sub in im_Item_.im_item_subcategory)
            {
                im_sub.item_subclass_id = Guid.CreateVersion7();
                im_sub.item_class_id = im_Item_.item_class_id;
                im_sub.company_id = im_Item_.company_id;
                im_sub.description = im_Item_.description;
                im_sub.edit_date_time = DateTime.Now;
                im_sub.edit_user_id = im_Item_.edit_user_id;
                im_sub.status = im_sub.status;
            }
            _context.im_item_Category.Add(im_Item_);


            await _context.SaveChangesAsync();
            return new ServiceResult<im_item_Category>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = im_Item_
            };
        }
        public async Task<ServiceResult<im_item_subcategory>> Create_sub_category(im_item_subcategory subcategory, string item_class_id)
        {
            if (subcategory == null || item_class_id == null)
            {
                return new ServiceResult<im_item_subcategory>
                {
                    Success = false,
                    Message = "No data found to insert",
                    Status = -1,
                };
            }
            var guid_item_class_id=Guid.Parse(item_class_id);
            var category = await _context.im_item_Category.Include(a => a.im_item_subcategory).FirstOrDefaultAsync(a => a.item_class_id == guid_item_class_id);
           
            im_item_Category im_Item_Category = new im_item_Category();

            subcategory.item_subclass_id = Guid.CreateVersion7();
            subcategory.item_class_id = category.item_class_id;
            subcategory.company_id = category.company_id;
            subcategory.description = subcategory.description;
            subcategory.edit_user_id = subcategory.edit_user_id;
            subcategory.edit_date_time = DateTime.Now;
            subcategory.status = subcategory.status;

            category.im_item_subcategory.Add(subcategory);

            await _context.SaveChangesAsync();
            return new ServiceResult<im_item_subcategory>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = subcategory
            };

        }
        public async Task<ServiceResult<List<im_item_Category>>> categoryList()
        {
            if (_context.im_item_Category == null)
            {
                return new ServiceResult<List<im_item_Category>>
                {
                    Success = false,
                    Message = "NO data found",
                    Status = -1
                };
            }
            var Category = await _context.im_item_Category.Include(a => a.im_item_subcategory).ToListAsync();
            return new ServiceResult<List<im_item_Category>>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = Category
            };
        }
        public async Task<ServiceResult<im_item_Category>> category_list_id(string item_class_id)
        {
            if (_context.im_item_Category == null)
            {
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "NO data found",
                    Status = -1
                };
            }
            var guid_item_class_id = Guid.Parse(item_class_id);

            var Category = await _context.im_item_Category.Include(a => a.im_item_subcategory).FirstOrDefaultAsync(a => a.item_class_id == guid_item_class_id);
            return new ServiceResult<im_item_Category>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = Category
            };
        }
        public async Task<ServiceResult<im_item_Category>> Update(im_item_Category im_Item_, string item_class_id)
        {
            if (im_Item_ == null)
            {
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "No data found to update",
                    Status = -1,
                };
            }
            var guid_item_class_id = Guid.Parse(item_class_id);

            var category = await _context.im_item_Category
                .Include(a => a.im_item_subcategory)
                .FirstOrDefaultAsync(a => a.item_class_id == guid_item_class_id);

            if (category == null)
            {
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "Category not found",
                    Status = -1,
                };
            }

            category.company_id = im_Item_.company_id;
            category.item_class = im_Item_.item_class;
            category.description = im_Item_.description;
            category.categoryType = im_Item_.categoryType;
            category.item_count = im_Item_.item_count;
            category.Sales_count = im_Item_.Sales_count;
            category.code = im_Item_.item_class?.Substring(0, 3).ToUpper();
            //category.item_cat_id = im_Item_.item_cat_id;
            category.edit_user_id = im_Item_.edit_user_id;
            category.edit_date_time = DateTime.Now;
            category.categoryImage = im_Item_.categoryImage;
            category.status = im_Item_.status;
            category.publish = im_Item_.publish;

            foreach (var im_sub in im_Item_.im_item_subcategory)
            {
                var item_sub = category.im_item_subcategory.FirstOrDefault(v => v.item_subclass_id == im_sub.item_subclass_id);

                item_sub.item_class_id = im_sub.item_class_id;
                item_sub.company_id = im_sub.company_id;
                item_sub.description = im_sub.description;
                item_sub.edit_date_time = DateTime.Now;
                item_sub.edit_user_id = im_sub.edit_user_id;
            }
            _context.im_item_Category.Update(category);

            await _context.SaveChangesAsync();

            return new ServiceResult<im_item_Category>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = category
            };
        }
        public async Task<ServiceResult<im_item_Category>> Delete(string item_class_id)
        {
            if(item_class_id == null)
            {
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "Not found",
                    Status=-1
                };
            }
            var guid_item_class_id = Guid.Parse(item_class_id);

            var category = await _context.im_item_Category.Include(a => a.im_item_subcategory).FirstOrDefaultAsync(a => a.item_class_id == guid_item_class_id);
            foreach(var su_catg in category.im_item_subcategory)
            {
                _context.im_item_subcategory.Remove(su_catg);
            }
            _context.im_item_Category.Remove(category);
            await _context.SaveChangesAsync();
            return new ServiceResult<im_item_Category>
            {
                Success = true,
                Message = "Deleted ",
                Status = 1
            };
        }
    }
}

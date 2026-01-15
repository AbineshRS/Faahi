using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using Faahi.Model.Stores;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.im_products.category
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ApplicationDbContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<im_item_Category>> Create_category(im_item_Category im_Item_)
        {
            if (im_Item_ == null)
            {
                _logger.LogWarning("Create_category called with null im_Item_");
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "NO data found to insert",
                    Status = -1,

                };
            }
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating category");
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }


        }
        public async Task<ServiceResult<im_item_subcategory>> Create_sub_category(im_item_subcategory subcategory, string item_class_id)
        {
            if (subcategory == null || string.IsNullOrWhiteSpace(item_class_id))
            {
                _logger.LogWarning("Create_sub_category called with null subcategory or empty item_class_id");
                return new ServiceResult<im_item_subcategory>
                {
                    Success = false,
                    Message = "No data found to insert",
                    Status = -1,
                };
            }
            try
            {
                // Safely parse the incoming item_class_id
                if (!Guid.TryParse(item_class_id, out Guid guid_item_class_id))
                {
                    return new ServiceResult<im_item_subcategory>
                    {
                        Success = false,
                        Message = "Invalid item_class_id",
                        Status = -1,
                    };
                }

                // Load the category with tracking
                var category = await _context.im_item_Category
                    .Include(c => c.im_item_subcategory) // Include the navigation
                    .FirstOrDefaultAsync(c => c.item_class_id == guid_item_class_id);

                if (category == null)
                {
                    return new ServiceResult<im_item_subcategory>
                    {
                        Success = false,
                        Message = "Item category not found",
                        Status = -1,
                    };
                }

                // Assign necessary fields
                subcategory.item_subclass_id = Guid.NewGuid(); // Or use Guid.CreateVersion7() if available
                subcategory.item_class_id = category.item_class_id; // Foreign key
                subcategory.company_id = category.company_id;
                subcategory.edit_date_time = DateTime.Now;

                // Add to the parent category's navigation property
                category.im_item_subcategory.Add(subcategory);
                _context.Entry(subcategory).State = EntityState.Added;

                // Save all changes
                await _context.SaveChangesAsync();

                return new ServiceResult<im_item_subcategory>
                {
                    Success = true,
                    Message = "Subcategory created successfully",
                    Status = 1,
                    Data = subcategory
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating subcategory");
                return new ServiceResult<im_item_subcategory>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }

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
                _logger.LogWarning("Update called with null im_Item_");
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "No data found to update",
                    Status = -1,
                };
            }
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category");
                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }

        }
        public async Task<ServiceResult<im_item_Category>> Delete(string item_class_id)
        {
            if (item_class_id == null)
            {

                return new ServiceResult<im_item_Category>
                {
                    Success = false,
                    Message = "Not found",
                    Status = -1
                };
            }
            var guid_item_class_id = Guid.Parse(item_class_id);

            var category = await _context.im_item_Category.Include(a => a.im_item_subcategory).FirstOrDefaultAsync(a => a.item_class_id == guid_item_class_id);
            foreach (var su_catg in category.im_item_subcategory)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="im_ProductCategories"></param>
        /// <returns></returns>
        public async Task<ServiceResult<im_ProductCategories>> Create_product_category(im_ProductCategories im_ProductCategories)
        {
            if (im_ProductCategories == null)
            {
                _logger.LogWarning("Create_product_category called with null im_ProductCategories");
                return new ServiceResult<im_ProductCategories>
                {
                    Success = false,
                    Message = "NO data found to insert",
                    Status = -1,
                };
            }
            try
            {
                //var im_ProductCategoriesExist = await _context.im_ProductCategories
                //    .FirstOrDefaultAsync(c => c.category_name.ToLower() == im_ProductCategories.category_name.ToLower());
                //if (im_ProductCategoriesExist != null)
                //{
                //    return new ServiceResult<im_ProductCategories>
                //    {
                //        Success = false,
                //        Message = "Category already exists",
                //        Status = -2
                //    };
                //}

                im_ProductCategories.category_id = Guid.CreateVersion7();
                im_ProductCategories.category_name = im_ProductCategories.category_name;
                //im_ProductCategories.parent_id = im_ProductCategories.parent_id;
                im_ProductCategories.image_url = im_ProductCategories.image_url;
                im_ProductCategories.edit_user_id = im_ProductCategories.edit_user_id;
                im_ProductCategories.edit_date_time = DateTime.Now;
                im_ProductCategories.is_active = im_ProductCategories.is_active;
                _context.im_ProductCategories.Add(im_ProductCategories);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_ProductCategories>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = im_ProductCategories
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product category");
                return new ServiceResult<im_ProductCategories>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }
        }
        public async Task<ServiceResult<im_ProductCategories>> Create_sub_product_categories(im_ProductCategories im_ProductCategories)
        {
            if (im_ProductCategories == null)
            {
                _logger.LogWarning("Update_product_category called with null im_ProductCategories");
                return new ServiceResult<im_ProductCategories>
                {
                    Success = false,
                    Message = "No data found to update",
                    Status = 400,
                };
            }
            try
            {
                var category = await _context.im_ProductCategories.FirstOrDefaultAsync(a => a.category_id == im_ProductCategories.category_id);
                if (category == null)
                {
                    return new ServiceResult<im_ProductCategories>
                    {
                        Success = false,
                        Message = "Category not found",
                        Status = 400,
                    };
                }
                im_ProductCategories.category_id = Guid.CreateVersion7();
                im_ProductCategories.category_name = im_ProductCategories.category_name;
                im_ProductCategories.image_url = im_ProductCategories.image_url;
                im_ProductCategories.parent_id = category.category_id;
                //category.edit_user_id = im_ProductCategories.edit_user_id;
                im_ProductCategories.edit_date_time = im_ProductCategories.edit_date_time;
                im_ProductCategories.is_active = im_ProductCategories.is_active;
                _context.im_ProductCategories.Add(im_ProductCategories);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_ProductCategories>
                {
                    Success = true,
                    Message = "Success",
                    Status = 201,
                    Data = category
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product category");
                return new ServiceResult<im_ProductCategories>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = 500,
                };
            }
        }
        public async Task<ServiceResult<im_ProductCategories>> Delete_product_category(Guid category_id)
        {
            if (category_id == null)
            {
                return new ServiceResult<im_ProductCategories>
                {
                    Success = false,
                    Message = "Not found",
                    Status = 400
                };
            }
            try
            {
                var category = await _context.im_ProductCategories.Where(a => a.category_id == category_id || a.parent_id == category_id).ToListAsync();

                _context.im_ProductCategories.RemoveRange(category);


                await _context.SaveChangesAsync();
                return new ServiceResult<im_ProductCategories>
                {
                    Success = true,
                    Message = "Deleted ",
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product category");
                return new ServiceResult<im_ProductCategories>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = 500,
                };
            }

        }
        public async Task<ServiceResult<List<im_ProductCategories>>> Get_all_product_category()
        {
            if (_context.im_ProductCategories == null)
            {
                return new ServiceResult<List<im_ProductCategories>>
                {
                    Success = false,
                    Message = "NO data found",
                    Status = -1
                };
            }
            try
            {
                int opr = 1; 
                var categories = await _context.im_ProductCategories
                    .FromSqlRaw("EXEC dbo.GetAllProductCategories @opr={0}", opr)
                    .AsNoTracking()
                    .ToListAsync();
                //var Category = await _context.im_ProductCategories.FromSqlRaw("EXEC dbo.GetAllProductCategories").AsNoTracking().ToListAsync();

                return new ServiceResult<List<im_ProductCategories>>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = categories
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product categories");
                return new ServiceResult<List<im_ProductCategories>>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }

        }
        public async Task<ServiceResult<List<st_StoreCategories>>> Create_StoreCategories(List<st_StoreCategories> st_StoreCategories)
        {
            if (st_StoreCategories == null)
            {
                _logger.LogWarning("Create_StoreCategories called with null st_StoreCategories");
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = false,
                    Message = "NO data found to insert",
                    Status = -1,
                };
            }
            try
            {
                var existingcategories = await _context.st_StoreCategories.Where(a => a.store_id == st_StoreCategories.First().store_id && a.category_id == st_StoreCategories.First().category_id).ToListAsync();
                if (existingcategories.Any())
                {
                    return new ServiceResult<List<st_StoreCategories>>
                    {
                        Success = false,
                        Message = "Some categories are already assigned to this store.",
                        Status = -2
                    };
                }

                foreach (var category in st_StoreCategories)
                {
                    category.store_category_id = Guid.CreateVersion7();
                    category.store_id = category.store_id;
                    category.category_id = category.category_id;
                    category.is_selected = category.is_selected;
                    _context.st_StoreCategories.Add(category);

                }

                await _context.SaveChangesAsync();
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = st_StoreCategories
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating store category");
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }
        }
    }
}

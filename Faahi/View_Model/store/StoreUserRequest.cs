using Faahi.Model.st_sellers;
using Faahi.Model.Stores;

namespace Faahi.View_Model.store
{
    public class StoreUserRequest
    {
        public st_store_add st_stores { get; set; } = new();
        public List<st_StoreCategories> StoreCategories { get; set; } = new();
    }
    public class StoreCategoryViewModel
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreLocation { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public string StoreType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<CategoryViewModel> st_StoreCategories { get; set; } = new List<CategoryViewModel>();
    }

    public class CategoryViewModel
    {
        public Guid? store_category_id { get; set; }

        public Guid? store_id { get; set; }
        public Guid? category_id { get; set; }
        public string? is_selected { get; set; } = null;
    }

}

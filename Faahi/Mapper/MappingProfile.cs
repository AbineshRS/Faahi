using AutoMapper;
using Faahi.Model.im_products;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.View_Model.store;

namespace Faahi.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Map st_store → st_store_view automatically
            CreateMap<st_stores, st_store_view>();
            CreateMap<st_StoreCategories, st_StoreCategories_view>();
            CreateMap<im_ProductCategories, im_ProductCategories_view>();
        }
    }
}

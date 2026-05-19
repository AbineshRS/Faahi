
using AutoMapper;
using Faahi.Dto.temp;
using Faahi.Model.temp_tables;

namespace Faahi.Service.Mappper.Inventory_adjustment
{
    public class temp_stock_ad_lines_mapper:Profile
    {
        public temp_stock_ad_lines_mapper()
        {
            CreateMap<temp_stock_ad_lines, temp_stock_ad_lines_dto>().ReverseMap();
        }
    }
}

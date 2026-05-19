using AutoMapper;
using Faahi.Dto.Inventory_adjustment;
using Faahi.Dto.Inventory_adjustment.adjustment_rejection;
using Faahi.Model.im_products;

namespace Faahi.Service.Mappper.Inventory_adjustment
{
    public class inventory_adjustment_header_mapper:Profile
    {
        public inventory_adjustment_header_mapper()
        {
            CreateMap<inventory_adjustment_header,inventory_adjustment_header_dto>().ReverseMap();

            CreateMap<inventory_adjustment_lines, inventory_adjustment_lines_dto>().ReverseMap();

            CreateMap<inventory_adjustment_lines, inventory_adjustment_lines_update_dto>().ReverseMap();

            CreateMap<im_random_Stock_reject, im_random_Stock_reject_dto>().ReverseMap();
        }
    }
}

using AutoMapper;
using Faahi.Dto.Purchase_dto;
using Faahi.Model.im_products;

namespace Faahi.Service.Mappper
{
    public class StockTransferHeader_mapper : Profile
    {
        public StockTransferHeader_mapper()
        {
            CreateMap<im_StockTransferHeader, im_StockTransferHeader_dto>();

            CreateMap<im_StockTransferHeader_dto, im_StockTransferHeader>()
                .ForMember(d => d.FromStore, o => o.Ignore())
                .ForMember(d => d.ToStore, o => o.Ignore())
                .ForMember(d => d.co_Business, o => o.Ignore());

            CreateMap<im_StockTransferLines, im_StockTransferLines_dto>()
                .ReverseMap();
        }
    }
}

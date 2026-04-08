using Faahi.Dto;
using Faahi.Model.am_users;
using Faahi.Model.Order;

namespace Faahi.Service.market_place
{
    public interface IMarket_place_service
    {
        Task<ServiceResult<am_users>> Add_users(am_users users);

        Task<ServiceResult<om_OrderSources>> Add_source(om_OrderSources om_OrderSources);

        Task<ServiceResult<List<am_users>>> Get_market_place_users();

        Task<ServiceResult<List<om_OrderSources>>> Get_sources(Guid business_id);
    }
}

using Faahi.Dto;
using Faahi.Dto.am_users;
using Faahi.Dto.mk_blacklisted;
using Faahi.Dto.om_Orders;
using Faahi.Model.am_users;
using Faahi.Model.Order;
using Faahi.Model.site_settings;

namespace Faahi.Service.market_place
{
    public interface IMarket_place_service
    {
        Task<ServiceResult<am_users>> Add_users(am_users users);

        Task<ServiceResult<om_OrderSources>> Add_source(om_OrderSources om_OrderSources);

        Task<ServiceResult<List<am_users_dto>>> Get_market_place_users(string search_text);

        Task<ServiceResult<List<om_OrderSources>>> Get_sources(Guid business_id);

        Task<ServiceResult<mk_customer_addresses>> Add_shipping_address(mk_customer_addresses shipping_address);

        Task<ServiceResult<List<mk_customer_addresses>>> Get_shipping( string search_text);

        Task<ServiceResult<mk_business_zones>> Add_Zones(mk_business_zones business_zones);

        Task<ServiceResult<List<mk_business_zones>>> get_zones(Guid business_id);

        Task<ServiceResult<List<om_CustomerOrders_dto>>> Get_order_list(Guid business_id);

        Task<ServiceResult<mk_blacklisted_numbers_dto>> check_black_list(Guid business_id,string phone_number);
    }
}

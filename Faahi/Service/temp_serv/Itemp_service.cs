using Faahi.Dto;
using Faahi.Model.temp_tables;

namespace Faahi.Service.temp_serv
{
    public interface Itemp_service
    {
        Task<ServiceResult<List<temp_im_variant>>> Add_temp_varient(List<temp_im_variant> varient);

        Task<ServiceResult<List<temp_im_variant>>> get_tempvariant(Guid store_id);
    }
}

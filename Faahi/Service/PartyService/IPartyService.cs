using Faahi.Dto;
using Faahi.Model.Shared_tables;

namespace Faahi.Service.PartyService
{
    public interface IPartyService
    {
        Task<ServiceResult<st_Parties>> Create_partys(st_Parties parties);
    }
}

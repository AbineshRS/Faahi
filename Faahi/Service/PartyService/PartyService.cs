using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using Faahi.Model.Shared_tables;
using Faahi.Service.im_products;

namespace Faahi.Service.PartyService
{
    public class PartyService:IPartyService
    {
        private readonly ApplicationDbContext _context;

        public PartyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<st_Parties>> Create_partys(st_Parties parties)
        {
            if (parties == null)
            {
                return new ServiceResult<st_Parties>
                {
                    Success = false,
                    Message = "No data found",
                    Status = -1
                };
            }
            //var table = "st_Parties";
            //var am_table = await _context.am_table_next_key.FindAsync(table);
            //var next_key = Convert.ToInt16(am_table.next_key);

            //var table2 = "st_PartyRoles";
            //var am_table2 = await _context.am_table_next_key.FindAsync(table2);
            //var next_key2 = Convert.ToInt16(am_table2.next_key);

            //var table3 = "st_PartyAddresses";
            //var am_table3 = await _context.am_table_next_key.FindAsync(table3);
            //var next_key3 = Convert.ToInt16(am_table3.next_key);

            //var table4 = "st_Parties";
            //var am_table4 = await _context.am_table_next_key.FindAsync(table4);
            //var next_key4 = Convert.ToInt16(am_table4.next_key);

            parties.party_id = Guid.CreateVersion7();
            parties.party_type = parties.party_type;
            parties.display_name=parties.display_name;
            parties.legal_name=parties.legal_name;
            parties.payable_name=parties.payable_name;
            parties.tax_id=parties.tax_id;
            parties.email=parties.email;
            parties.phone=parties.phone;
            parties.default_currency=parties.default_currency;
            parties.created_at=DateTime.Now;
            parties.updated_at=DateTime.Now;
            parties.status=parties.status;

            foreach(var st_roles in parties.st_PartyRoles)
            {
                st_roles.party_role_id= Guid.CreateVersion7(); 
                st_roles.party_id = parties.party_id;
                st_roles.role=st_roles.role;
                st_roles.created_at = DateTime.Now;

                foreach(var party_address in st_roles.st_PartyAddresses)
                {
                    party_address.address_id=Guid.CreateVersion7();
                    party_address.party_id=parties.party_id;
                    party_address.address_type=party_address.address_type;
                    party_address.line1 = party_address.line1;
                    party_address.line2= party_address.line2;
                    party_address.region=party_address.region;
                    party_address.postal_code=party_address.postal_code;
                    party_address.country=party_address.country;
                    party_address.latitude=party_address.latitude;
                    party_address.longitude=party_address.longitude;
                    party_address.created_at=DateTime.Now;
                    party_address.updated_at=DateTime.Now;
                    party_address.is_default = party_address.is_default;

                    foreach(var party_contact in party_address.PartyContacts)
                    {
                        party_contact.contact_id = Guid.CreateVersion7();
                        party_contact.party_id = parties.party_id;
                        party_contact.first_name=party_contact.first_name;
                        party_contact.last_name=party_contact.last_name;
                        party_contact.email=party_contact.email;
                        party_contact.phone=party_contact.phone;
                        party_contact.title=party_contact.title;
                        party_contact.created_at= DateTime.Now;
                        party_contact.updated_at = DateTime.Now;
                        party_contact.is_primary=party_contact.is_primary;
                    }
                }
            }
            _context.st_Parties.Add(parties);

           
            await _context.SaveChangesAsync();

            return new ServiceResult<st_Parties>
            {
                Success = true,
                Message = " created successfully",
                Status=1,
                Data = parties
            };
        }
    }
}

namespace Faahi.Dto.Auth
{
    public class Login_respose_dto
    {
        public List<Guid>? store_id {  get; set; }

        public Guid? company_id {  get; set; }
        public List<string>? store_names { get; set; }

    }
}

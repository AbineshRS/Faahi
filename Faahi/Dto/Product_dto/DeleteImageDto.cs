namespace Faahi.Dto.Product_dto
{
    public class DeleteImageDto
    {
        public Guid Product_Id { get; set; }
        public Guid Variant_Id { get; set; }
        public List<string> Deleted_Images { get; set; } = new();
    }
}

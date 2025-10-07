namespace Faahi.Dto
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Int32 Status { get; set; }
        public T? Data { get; set; }
    }

}

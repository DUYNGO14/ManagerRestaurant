namespace ManagerRestaurant.Domain.Model
{
    public class ResponseApi
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public object? Data { get; set; }
    }
}

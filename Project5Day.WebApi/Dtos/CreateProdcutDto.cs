namespace Project5Day.WebApi.Dtos
{
    public class CreateProdcutDto
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}

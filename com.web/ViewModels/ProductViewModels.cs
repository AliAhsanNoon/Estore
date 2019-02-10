namespace com.web.ViewModels
{
    public class ProductViewModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Category_ID { get; set; }
        public bool isFeatured { get; set; }
        public string ImageURL { get; set; }
    }
}
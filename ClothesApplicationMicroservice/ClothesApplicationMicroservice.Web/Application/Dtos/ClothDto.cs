namespace ClothesApplicationMicroservice.Web.Application.Dtos
{
    public class ClothDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public bool isUserOwn { get; set; }
    }
}

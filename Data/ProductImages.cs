using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public List<IFormFile>? Image { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product? Product { get; set; }
    }
}

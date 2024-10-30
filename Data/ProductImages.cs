using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}

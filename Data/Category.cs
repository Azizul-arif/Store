using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Category")]
        [MaxLength (25)]
        public string CategoryName { get; set; }
        public int ParentID { get; set; }
    }
}

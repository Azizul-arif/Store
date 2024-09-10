using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data
{
	public class Color
	{
		public int Id { get; set; }
		[Required]
		[Display(Name="Color")]
		public string Name { get; set; }
		
	}
	public class Size
	{
		public int Id { get; set; }
		[Required]
		[Display(Name="Size")]
		public string Name { get; set; }
	}
}

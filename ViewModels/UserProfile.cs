namespace Ecommerce.ViewModels
{
    
    public class UserProfile
    {
        public string? FullName { get; set; }
        public string? PicPath { get; set; }
        public IFormFile? ProfilePic { get; set; }
    }
}

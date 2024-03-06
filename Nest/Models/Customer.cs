using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Customer : BaseAuditable
    {
        public string FullName { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LivingAddress { get; set; }
        public string IpAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string? Image { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}

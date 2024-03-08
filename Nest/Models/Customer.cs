using Nest.Models.BaseEntitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nest.Models
{
    public class Customer : BaseAuditable
    {

        [NotMapped]
        public IFormFile? FormFile { get; set; }
        public string FullName { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LivingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string? Image { get; set; }

        //public List<Product> Products { get; set; } = null!;
        public List<CustomerRating> CustomerRatings { get; set; } = null!;
    }
}

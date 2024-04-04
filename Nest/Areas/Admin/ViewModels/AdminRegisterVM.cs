using System.ComponentModel.DataAnnotations;

namespace Nest.Areas.Admin.ViewModels
{
    public class AdminRegisterVM
    {
        public string Name { get; set; }
        public string Username { get; set; }

        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

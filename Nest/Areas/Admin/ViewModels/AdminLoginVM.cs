using System.ComponentModel.DataAnnotations;

namespace Nest.Areas.Admin.ViewModels
{
    public class AdminLoginVM
    {
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Nest.Areas.Admin.ViewModels
{
    public class AuthorizationVM
    {
        [DataType(DataType.Text)]
        public string Code { get; set; } = null!;
    }
}

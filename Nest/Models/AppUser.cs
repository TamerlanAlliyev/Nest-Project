
using Microsoft.AspNetCore.Identity;

namespace Nest.Models;

public class AppUser:IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName { get; set; }


}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RedirectIfInAdminAreaAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionDescriptor.RouteValues.ContainsKey("area"))
        {
            var areaName = context.ActionDescriptor.RouteValues["area"];
            if (areaName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new RedirectToActionResult("Index", "AdminAccount", new { area = "Admin" });
            }
        }
        base.OnActionExecuting(context);
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanCentral.API.Utility.Middleware;
public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}
public class ClaimRequirementFilter : IAsyncAuthorizationFilter
{
    readonly Claim _claim;

    public ClaimRequirementFilter(Claim claim)
    {
        _claim = claim;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}

public class MyClaimTypes
{    
    public const string MENU = "Menu";
    public const string PERMISSION1R = "F1CodeR";
    public const string PERMISSION1W = "F1CodeW";

    public const string PERMISSION2R = "F2CodeR";
    public const string PERMISSION2W = "F2CodeW";

    public const string PERMISSION3R = "F3CodeR";
    public const string PERMISSION3W = "F3CodeW";

    public const string PERMISSION4R = "F4CodeR";
    public const string PERMISSION4W = "F4CodeW";

    public const string PERMISSION5R = "F5CodeR";
    public const string PERMISSION5W = "F5CodeW";

    public const string PERMISSION6R = "F6CodeR";
    public const string PERMISSION6W = "F6CodeW";

    public const string PERMISSION7R = "F7CodeR";
    public const string PERMISSION7W = "F7CodeW";

    public const string PERMISSION8R = "F8CodeR";
    public const string PERMISSION8W = "F8CodeW";

    public const string PERMISSION9R = "F9CodeR";
    public const string PERMISSION9W = "F9CodeW";

    public const string PERMISSION10R = "F10CodeR";
    public const string PERMISSION10W = "F10CodeW";

    public const string PERMISSION11R = "F11CodeR";
    public const string PERMISSION11W = "F11CodeW";

    public const string PERMISSION12R = "F12CodeR";
    public const string PERMISSION12W = "F12CodeW";

    public const string PERMISSION13R = "F13CodeR";
    public const string PERMISSION13W = "F13CodeW";

    public const string PERMISSION14R = "F14CodeR";
    public const string PERMISSION14W = "F14CodeW";

    public const string PERMISSION15R = "F15CodeR";
    public const string PERMISSION15W = "F15CodeW";
}

public class ContactClaims
{
    public const string TRUE = "true";
    public const string FALSE = "false";
}

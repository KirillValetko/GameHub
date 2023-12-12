using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Web.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresRoleClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimValue;

        public RequiresRoleClaimAttribute(string claimValue)
        {
            _claimValue = claimValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInRole(_claimValue))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

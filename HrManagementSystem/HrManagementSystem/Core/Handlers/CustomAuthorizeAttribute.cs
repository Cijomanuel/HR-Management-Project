using Microsoft.AspNetCore.Authorization;

namespace HrManagementSystem.Core.Handlers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute() : base(PolicyName.DynamicPermissions )
        {

        }

        public CustomAuthorizeAttribute(string policy)
        {
            Policy = $"{policy}";
        }
    }
    public static class PolicyName
    {
        public const string OrHasClaim = nameof(OrHasClaim);
        public const string DynamicPermissions = nameof(DynamicPermissions);
    }
}

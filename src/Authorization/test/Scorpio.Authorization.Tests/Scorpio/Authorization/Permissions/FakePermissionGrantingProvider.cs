using System;
using System.Threading.Tasks;

namespace Scorpio.Authorization.Permissions
{
    internal class FakePermissionGrantingProvider : IPermissionGrantingProvider
    {
        public string Name { get; } = "FakeProvider";

        public Task<PermissionGrantingInfo> GrantAsync(PermissionGrantingContext context)
        {
            var success = context.Permission.Name.IsIn("Permission_Test_1", "Permission_Test_3")
                && context?.Principal?.Identity?.Name == "FakeUser";
            return Task.FromResult(new PermissionGrantingInfo(success, Name));
        }
    }
}

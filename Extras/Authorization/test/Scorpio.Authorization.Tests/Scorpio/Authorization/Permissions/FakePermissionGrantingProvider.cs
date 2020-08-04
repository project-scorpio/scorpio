using System.Threading.Tasks;

namespace Scorpio.Authorization.Permissions
{
    class FakePermissionGrantingProvider : IPermissionGrantingProvider
    {
        public string Name { get; } = "FakeProvider";

        public Task<PermissionGrantingInfo> GrantAsync(PermissionGrantingContext context)
        {
            var success = context.Permission.Name == "Permission_Test_1"
                && context?.Principal?.Identity?.Name == "FakeUser";
            return Task.FromResult(new PermissionGrantingInfo(success, Name));
        }
    }
}

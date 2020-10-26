namespace Scorpio.Authorization.Permissions
{
    public class FakePermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        public void Define(IPermissionDefinitionContext context)
        {
            context.AddGroup("PermissionGroup_Test_1").AddPermission("Permission_Test_1", p => p.AddChild("Permission_Test_2")).AddPermission("Permission_Test_3");
        }
    }
}

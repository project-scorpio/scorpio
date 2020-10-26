using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    [Authorize("Permission_Test_1.Permission_Test_2")]
    public interface IAuthorizeAttributeTestService
    {
        Task AuthorizeByServcieAsync();

        [Authorize("Permission_Test_1")]
        Task AuthorizeByAttributeAsync();

        [Authorize("Permission_Test_1", "Permission_Test_1.Permission_Test_2", RequireAllPermissions = true)]
        Task AuthorizeByNotAllAttributeAsync();
        [Authorize("Permission_Test_1", "Permission_Test_3", RequireAllPermissions = true)]
        Task AuthorizeByAllAttributeAsync();

        [AllowAnonymous]
        Task AuthorizeAnonymousAsync();
    }
}

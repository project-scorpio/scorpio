using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    [Authorize("Permission_Test_2")]
    public interface IAuthorizeAttributeTestService
    {
        Task AuthorizeByServcieAsync();

        [Authorize("Permission_Test_1")]
        Task AuthorizeByAttributeAsync();
        [AllowAnonymous]
        Task AuthorizeAnonymousAsync();
    }
}

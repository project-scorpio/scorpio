using System.Security.Principal;

using Scorpio.Security;

namespace Scorpio.Authorization
{
    class FakePrincipalAccessor : ICurrentPrincipalAccessor
    {
        public IPrincipal Principal { get; }

        public FakePrincipalAccessor()
        {
            Principal = new GenericPrincipal(new GenericIdentity("FakeUser"), new[] { "FakeRole" });
        }
    }
}

using System.Security.Principal;

using Scorpio.Security;

namespace Scorpio.Authorization
{
    internal class FakePrincipalAccessor : ICurrentPrincipalAccessor
    {
        public void SetPrincipal(IPrincipal principal) => Principal = principal;
        public IPrincipal Principal { get; private set; }

        public FakePrincipalAccessor() => Principal = new GenericPrincipal(new GenericIdentity("FakeUser"), new[] { "FakeRole" });
    }
}

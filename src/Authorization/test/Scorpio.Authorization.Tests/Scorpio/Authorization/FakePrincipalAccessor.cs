using System.Security.Principal;

using Scorpio.Security;

namespace Scorpio.Authorization
{
    internal class FakePrincipalAccessor : ICurrentPrincipalAccessor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:使用自动属性", Justification = "<挂起>")]
        private IPrincipal _principal;
        public void SetPrincipal(IPrincipal principal) => _principal = principal;
        public IPrincipal Principal => _principal;

        public FakePrincipalAccessor() => _principal = new GenericPrincipal(new GenericIdentity("FakeUser"), new[] { "FakeRole" });
    }
}

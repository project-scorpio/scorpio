using Scorpio.Security;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Scorpio.Authorization
{
    class FakePrincipalAccessor: ICurrentPrincipalAccessor
    {
        public IPrincipal Principal { get; }

        public FakePrincipalAccessor()
        {
            Principal = new GenericPrincipal(new GenericIdentity("FakeUser"), new[] { "FakeRole" });
        }
    }
}

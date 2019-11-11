using Scorpio.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.Auditing
{
    internal class AspNetCoreAuditInfoWapper : AuditInfoWapper
    {
        public AspNetCoreAuditInfoWapper(AuditInfo auditInfo) : base(auditInfo)
        {
        }

        public string HttpMethod
        {
            get => GetExtraProperty<string>(nameof(HttpMethod));
            set => SetExtraProperty(nameof(HttpMethod), value);
        }

        public string Url
        {
            get => GetExtraProperty<string>(nameof(Url));
            set => SetExtraProperty(nameof(Url), value);
        }
        public string ClientIpAddress
        {
            get => GetExtraProperty<string>(nameof(ClientIpAddress));
            set => SetExtraProperty(nameof(ClientIpAddress), value);
        }
        public string BrowserInfo
        {
            get => GetExtraProperty<string>(nameof(BrowserInfo));
            set => SetExtraProperty(nameof(BrowserInfo), value);
        }
        public int HttpStatusCode
        {
            get => GetExtraProperty<int>(nameof(HttpStatusCode));
            set => SetExtraProperty(nameof(HttpStatusCode), value);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Scorpio.Auditing;


namespace Scorpio.AspNetCore.Mvc.Auditing
{
    [Route("api/audit-test")]
    public class AuditTestController : Controller
    {
        private readonly AuditingOptions _options;

        public AuditTestController(IOptions<AuditingOptions> options)
        {
            _options = options.Value;
        }

        [Route("audit-success")]
        public IActionResult AuditSuccessForGetRequests()
        {
            return Ok();
        }

        [Route("audit-fail")]
        public IActionResult AuditFailForGetRequests()
        {
            throw new ScorpioException("Exception occurred!");
        }
        [Route("audit-fail-object")]
        public object AuditFailForGetRequestsReturningObject()
        {
            throw new ScorpioException("Exception occurred!");
        }
    }
}

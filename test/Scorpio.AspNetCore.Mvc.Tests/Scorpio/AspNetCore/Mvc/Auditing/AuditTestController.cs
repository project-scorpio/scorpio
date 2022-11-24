using Microsoft.AspNetCore.Mvc;


namespace Scorpio.AspNetCore.Mvc.Auditing
{
    [Route("api/audit-test")]
    public class AuditTestController : Controller
    {

        public AuditTestController()
        {
        }

        [Route("audit-success")]
        public IActionResult AuditSuccessForGetRequests() => Ok();

        [Route("audit-fail")]
        public IActionResult AuditFailForGetRequests() => throw new ScorpioException("Exception occurred!");
        [Route("audit-fail-object")]
        public object AuditFailForGetRequestsReturningObject() => throw new ScorpioException("Exception occurred!");
    }
}

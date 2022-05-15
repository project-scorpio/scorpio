using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scorpio.AspNetCore.Mvc.Auditing
{
    public class AuditTestPageModel : PageModel
    {

        public AuditTestPageModel()
        {
        }

        public IActionResult OnGetAuditSuccessForGetRequests() => new OkResult();

        public IActionResult OnGetAuditFailForGetRequests() => throw new ScorpioException("Exception occurred!");

        public ObjectResult OnGetAuditFailForGetRequestsReturningObject() => throw new ScorpioException("Exception occurred!");
    }
}

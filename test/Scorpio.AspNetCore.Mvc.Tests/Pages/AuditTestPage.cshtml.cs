
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Scorpio;

namespace Scorpio.AspNetCore.Auditing.Pages
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
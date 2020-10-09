using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

using Scorpio.Auditing;

namespace Scorpio.AspNetCore.Mvc.Auditing
{
    public class AuditTestPageModel : PageModel
    {
        private readonly AuditingOptions _options;

        public AuditTestPageModel(IOptions<AuditingOptions> options)
        {
            _options = options.Value;
        }

        public IActionResult OnGetAuditSuccessForGetRequests()
        {
            return new OkResult();
        }

        public IActionResult OnGetAuditFailForGetRequests()
        {
            throw new ScorpioException("Exception occurred!");
        }

        public ObjectResult OnGetAuditFailForGetRequestsReturningObject()
        {
            throw new ScorpioException("Exception occurred!");
        }
    }
}

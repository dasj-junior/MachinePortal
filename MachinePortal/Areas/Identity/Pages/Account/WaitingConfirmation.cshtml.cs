using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MachinePortal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class WaitingConfirmationModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

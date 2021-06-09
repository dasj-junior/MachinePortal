using System;
using System.ComponentModel.DataAnnotations;

namespace MachinePortal.Models
{
    public class ErrorViewModel
    {
        [Display(Name = "Request ID")]
        public string RequestId { get; set; }

        [Display(Name = "Show Request ID")]
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
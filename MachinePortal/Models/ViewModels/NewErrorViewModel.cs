using System;
using System.ComponentModel.DataAnnotations;

namespace MachinePortal.Models
{
    public class NewErrorViewModel
    {
        public string notificationMessage { get; set; }
        public string notificationIcon { get; set; }
        public string notificationType { get; set; }

        public NewErrorViewModel()
        {
        }

        public NewErrorViewModel(string notificationMessage, string notificationIcon, string notificationType)
        {
            this.notificationMessage = notificationMessage;
            this.notificationIcon = notificationIcon;
            this.notificationType = notificationType;
        }
    }
}
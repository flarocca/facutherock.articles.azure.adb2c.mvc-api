using System;

namespace FacuTheRock.Articles.Azure.AdB2C.MVC_API.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

using System.Collections.Generic;
using Commons.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Commons.ViewComponents
{
    public class PageAlertViewComponent : ViewComponent
    {

        public PageAlertViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            List<Message> messages = new List<Message>();

            string alertsJson = (string)TempData["PageAlerts"];

            if (alertsJson != null)
            {
                messages = JsonConvert.DeserializeObject<List<Message>>(alertsJson);
            }
            
            return View("LayoutPageAlert", messages);
        }
    }
}

using Commons.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Commons.Identity;
using DataTablesParser;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Commons.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }

        /// <summary>
        /// Agrega una alerta a la lista de alertas desde el controlador para ser mostrada en la vista que se envía.
        /// </summary>
        /// <param name="pageAlertType"></param>
        /// <param name="description"></param>
        public void AddPageAlerts(PageAlertType pageAlertType, string description)
        {
            string alertsJson = (string) TempData["PageAlerts"];

            List<Message> alertsList = new List<Message>();

            if (alertsJson != null)
            {
                alertsList = JsonConvert.DeserializeObject<List<Message>>(alertsJson);
            }

            alertsList.Add(new Message { Type = pageAlertType.ToString().ToLower(), ShortDesc = description });

            string newAlertsJson = JsonConvert.SerializeObject(alertsList);
            
            TempData["PageAlerts"] = newAlertsJson;
        }

        public enum PageAlertType
        {
            Error,
            Info,
            Warning,
            Success
        }

        /// <summary>
        /// Agrega un breadcrumb arriba a la derecha dentro del panel gris, con un nombre y un link.
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="urlPath"></param>
        public void AddBreadcrumb(string displayName, string urlPath, string icon = null)
        {
            List<Message> messages;

            if (ViewBag.Breadcrumb == null)
            {
                messages = new List<Message>();
            }
            else
            {
                messages = ViewBag.Breadcrumb as List<Message>;
            }

            if (messages == null) return;
            messages.Add(new Message {DisplayName = displayName, URLPath = urlPath, FontAwesomeIcon = icon});
            ViewBag.Breadcrumb = messages;
        }


        public JsonResult DataTable<T>(IQueryable<T> queryable) where T : class
        {
            var parser = new Parser<T>(Request.Form, queryable);

            var result = parser.Parse();

            var json = new JsonResult(result);

            return json;
        }

    }

    public abstract class CommonsController : BaseController
    {

    }
}
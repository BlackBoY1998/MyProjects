using CRMWebAPI.Interfaces;
using CRMWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMModelClassLiberary;
namespace CRMWebAPI.Controllers
{
    public class AddNotificationController : ApiController
    {
        INotification _INotification;
        public AddNotificationController()
        {
            _INotification = new NotificationData();
        }
        [HttpPost]
        public IHttpActionResult Addnotification(NotificationsTB notificationsTB)
        {
            int count = _INotification.AddNotification(notificationsTB);
            return Ok(count);
        }
     

    }
}

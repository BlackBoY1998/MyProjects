using CRMWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMWebAPI.Repository;
using CRMApplication.Filters;
using CRMModelClassLiberary;
using CRMApplication.Hubs;
using CRMApplication.ConsumeMethods;

namespace CRMApplication.Controllers
{
    [ValidateSuperAdminSession]
    public class AddNotificationController : Controller
    {
        private INotification _INotification;
        private NotificationConsume objnotification;

        public AddNotificationController()
        {
            _INotification = new NotificationData();
            objnotification = new NotificationConsume();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NotificationsTB NotificationsTB)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(NotificationsTB);
                }

                _INotification.DisableExistingNotifications();

                var Notifications = new NotificationsTB
                {
                    CreatedOn = DateTime.Now,
                    Message = NotificationsTB.Message,
                    NotificationsID = 0,
                    Status = "A",
                    FromDate = NotificationsTB.FromDate,
                    ToDate = NotificationsTB.ToDate
                };
                objnotification.AddNotification("Addnotification", Notifications);

                MyNotificationHub.Send();
                return RedirectToAction("Add", "AddNotification");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult AllNotification()
        {
            return View();
        }

        public ActionResult LoadNotificationData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var notificationdata = _INotification.ShowNotifications(sortColumn, sortColumnDir, searchValue);
                recordsTotal = notificationdata.Count();
                var data = notificationdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult DeActivateNotification(string NotificationID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(NotificationID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var result = _INotification.DeActivateNotificationByID(Convert.ToInt32(NotificationID));

                if (result)
                {
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
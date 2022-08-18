﻿using CRMModelClassLiberary;
using CRMWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace CRMWebAPI.Repository
{
    public class NotificationData : INotification
    {
        public int AddNotification(NotificationsTB entity)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    _context.NotificationsTBs.Add(entity);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DisableExistingNotifications()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    _context.Database.ExecuteSqlCommand("Usp_DisableExistingNotifications");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<NotificationsTB_ViewModel> ShowNotifications(string sortColumn, string sortColumnDir, string Search)
        {
            var _context = new ApplicationDbContext();

            var IQueryableNotifications = (from notification in _context.NotificationsTBs
                                           select new NotificationsTB_ViewModel
                                           {

                                               CreatedOn = SqlFunctions.DateName("day", notification.CreatedOn).Trim() + "/" +
                          SqlFunctions.StringConvert((double)notification.CreatedOn.Value.Month).TrimStart() + "/" +
                          SqlFunctions.DateName("year", notification.CreatedOn),
                                               Message = notification.Message,
                                               NotificationsID = notification.NotificationsID,
                                               Status = notification.Status,
                                               FromDate = SqlFunctions.DateName("day", notification.FromDate).Trim() + "/" +
                          SqlFunctions.StringConvert((double)notification.FromDate.Value.Month).TrimStart() + "/" +
                          SqlFunctions.DateName("year", notification.FromDate) + " " + SqlFunctions.DateName("hh", notification.FromDate).Trim() + ":" + SqlFunctions.DateName("n", notification.FromDate).Trim() + ":" + SqlFunctions.DateName("s", notification.FromDate).Trim(),
                                               ToDate = SqlFunctions.DateName("day", notification.ToDate).Trim() + "/" +
                          SqlFunctions.StringConvert((double)notification.ToDate.Value.Month).TrimStart() + "/" +
                          SqlFunctions.DateName("year", notification.ToDate) + " " + SqlFunctions.DateName("hh", notification.ToDate).Trim() + ":" + SqlFunctions.DateName("n", notification.ToDate).Trim() + ":" + SqlFunctions.DateName("s", notification.ToDate).Trim(),
                                               Min = SqlFunctions.DateDiff("n", notification.FromDate.ToString(), notification.ToDate.ToString())

                                           });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableNotifications = IQueryableNotifications.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableNotifications = IQueryableNotifications.Where(m => m.Message == Search || m.Message == Search);
            }

            return IQueryableNotifications;

        }

        public bool DeActivateNotificationByID(int NotificationID)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    var notification = _context.NotificationsTBs.FirstOrDefault(c => c.NotificationsID == NotificationID);
                    notification.Status = "D";
                    _context.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
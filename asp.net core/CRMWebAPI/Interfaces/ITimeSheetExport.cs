﻿using CRMModelClassLiberary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CRMWebAPI.Interfaces
{
    public interface ITimeSheetExport
    {
        DataSet GetReportofTimeSheet(DateTime? FromDate, DateTime? ToDate, int UserID);
        DataSet GetWeekTimeSheetDetails(int TimeSheetMasterID);
        List<Registration> ListofEmployees(int UserID);
        DataSet GetTimeSheetMasterIDTimeSheet(DateTime? FromDate, DateTime? ToDate, int UserID);
        string GetUsernamebyRegistrationID(int RegistrationID);
        DataSet GetTimeSheetMasterIDTimeSheet(DateTime? FromDate, DateTime? ToDate);
    }
}
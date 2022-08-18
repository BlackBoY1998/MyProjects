using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetManagementModel;

namespace TimesheetManagementInterfaceLibrary
{
    public interface IAudit
    {
        void InsertAuditData(AuditTB audittb);
    }
}

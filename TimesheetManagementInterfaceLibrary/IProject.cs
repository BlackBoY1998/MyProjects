using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetManagementModel;

namespace TimesheetManagementInterfaceLibrary
{
    public interface IProject
    {
        List<ProjectMaster> GetListofProjects();
        bool CheckProjectCodeExists(string ProjectCode);
        bool CheckProjectNameExists(string ProjectName);
        int SaveProject(ProjectMaster ProjectMaster);
        IQueryable<ProjectMaster> ShowProjects(string sortColumn, string sortColumnDir, string Search);
        bool CheckProjectIDExistsInTimesheet(int ProjectID);
        bool CheckProjectIDExistsInExpense(int ProjectID);
        int ProjectDelete(int ProjectID);
        int GetTotalProjectsCounts();
    }
}

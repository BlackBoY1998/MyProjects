//using CRMWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRMWebAPI.Repository
{
    public class ProjectRepository //: IProjectRepository
    {
        //    private readonly pgsqlDbcontext db = new pgsqlDbcontext();
        //    public async Task Add(ProjectMaster projectDetails)
        //    {
        //        db.ProjectMaster.Add(projectDetails);
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch(Exception ex)
        //        {

        //        }
        //    }

        //    public async Task Delete(int id)
        //    {
        //        try
        //        {
        //            ProjectMaster projectDetails = await db.ProjectMaster.FindAsync(id);
        //            db.ProjectMaster.Remove(projectDetails);
        //            await db.SaveChangesAsync();
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }
        //    }

        //    public async Task<ProjectMaster> GetProject(int id)
        //    {
        //        try
        //        {
        //            ProjectMaster projectDetails = await db.ProjectMaster.FindAsync(id);
        //            if (projectDetails == null)
        //            {
        //                return null;
        //            }
        //            return projectDetails;
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }
        //    }

        //    public async Task<IEnumerable<ProjectMaster>> GetProjects()
        //    {
        //        try
        //        {
        //            var projects = await db.ProjectMaster.ToListAsync();
        //            return projects.AsQueryable();
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }

        //    }

        //    public async Task Update(ProjectMaster projectDetails)
        //    {
        //        try
        //        {
        //            db.Entry(projectDetails).State = EntityState.Modified;
        //            await db.SaveChangesAsync();
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //    private bool EmployeeExists(int id)
        //    {
        //        return db.ProjectMaster.Count(e => e.ProjectId == id) > 0;
        //    }

      }
}

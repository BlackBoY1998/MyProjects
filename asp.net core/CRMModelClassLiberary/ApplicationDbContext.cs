using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CRMModelClassLiberary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(): base("name=TimesheetDBEntities")
        {
        }

        public DbSet<Registration> Registration { get; set; }
        public DbSet<Roles> Role { get; set; }
        public DbSet<ProjectMaster> ProjectMaster { get; set; }
        public DbSet<TimeSheetMaster> TimeSheetMaster { get; set; }
        public DbSet<TimeSheetDetails> TimeSheetDetails { get; set; }
        public DbSet<ExpenseModel> ExpenseModel { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<TimeSheetAuditTB> TimeSheetAuditTB { get; set; }
        public DbSet<ExpenseAuditTB> ExpenseAuditTB { get; set; }
        public DbSet<AuditTB> AuditTB { get; set; }
        public DbSet<DescriptionTB> DescriptionTB { get; set; }
        public DbSet<AssignedRoles> AssignedRoles { get; set; }
        public System.Data.Entity.DbSet<CRMModelClassLiberary.NotificationsTB> NotificationsTBs { get; set; }
    }
}
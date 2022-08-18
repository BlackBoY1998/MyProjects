using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Scalfolding_Asp.net_core.Models
{
    public partial class AKASHAPIContext : DbContext
    {
        public AKASHAPIContext()
        {
        }

        public AKASHAPIContext(DbContextOptions<AKASHAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomersN> CustomersNs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Emp> Emps { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Employee1> Employees1 { get; set; }
        public virtual DbSet<LoginTable> LoginTables { get; set; }
        public virtual DbSet<NewTable> NewTables { get; set; }
        public virtual DbSet<SomeTable> SomeTables { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TestLocation> TestLocations { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
        
        
                optionsBuilder.UseSqlServer("Data Source=NEXSUS-DV67\\CCNEXSUS;Initial Catalog=AKASHAPI;User ID=sa;Password=ccntspl@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerLocation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName).HasMaxLength(20);
            });

            modelBuilder.Entity<CustomersN>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CustomersN");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DepositoryType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Details).IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Landline)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PanCardNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Uccno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UCCNo");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.ToTable("EMP");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBB997D439ABD");

                entity.ToTable("Employee");

                entity.Property(e => e.Doj)
                    .HasColumnType("datetime")
                    .HasColumnName("DOJ")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee1>(entity =>
            {
                entity.ToTable("Employees");

                entity.HasIndex(e => e.DepartmentId, "IX_FK_DepartmentEmployee");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.Gender).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employee1s)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentEmployee");
            });

            modelBuilder.Entity<LoginTable>(entity =>
            {
                entity.ToTable("LoginTable");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NewAccountDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password3)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NewTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NewTable");

                entity.Property(e => e.TimeId).HasColumnName("Time_ID");

                entity.Property(e => e.TimeSlot)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Time_Slot");
            });

            modelBuilder.Entity<SomeTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SomeTable");

                entity.Property(e => e.Dt)
                    .HasColumnType("datetime")
                    .HasColumnName("dt");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StdId)
                    .HasName("PK__Student__55DCAE1F0CBAE877");

                entity.ToTable("Student");

                entity.Property(e => e.StdEmail).HasMaxLength(150);

                entity.Property(e => e.StdName).HasMaxLength(50);
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("Template");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("DOC");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Template1)
                    .IsRequired()
                    .HasColumnName("Template");
            });

            modelBuilder.Entity<TestLocation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TestLocation");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Time).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Landline)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserLog");

                entity.Property(e => e.LoginDateTime).HasColumnType("datetime");

                entity.Property(e => e.LoginLogoutRemarks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LogoutDateTime).HasColumnType("datetime");

                entity.Property(e => e.MachineName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserIpaddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UserIPAddress");

                entity.Property(e => e.UserLogId).ValueGeneratedOnAdd();

                entity.Property(e => e.WinLoginName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

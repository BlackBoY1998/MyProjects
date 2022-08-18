using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebAPI.Models
{
    public class CustomerDetailsContext:DbContext
    {
        public CustomerDetailsContext(DbContextOptions<CustomerDetailsContext> options):base(options)
        {
                
        }
        public DbSet<Customer> Customers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CrudAsp.netcore.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CrudAsp.netcore.Repository
{
    public class ProductRepository:IRepository<Product>
    {
        private readonly string ConnectionString;
        public ProductRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("DBinfo:ConnectionString");

        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(ConnectionString);
            }
        }

       public void Add(Product item)
        {
           using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Product(pname)value(@pname)",item);
            }
        }

        public IEnumerable<Product> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Product>("SELECT * FROM product");
            }
        }

        public Product FindBy(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Product>("SELECT * FROM product WHERE pid=@pid",new { pid = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE  FROM product WHERE pid=@pid", new { pid = id });
            }
        }

        public void update(Product item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query<Product>("UPDATE product SET pname=@pname where pid=@pid",item);
            }
        }
    }
}

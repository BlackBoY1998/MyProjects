using CrudAsp.netcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAsp.netcore.Repository
{
    public interface IRepository<T> where T:BaseEntity
    {
        void Add(T item);
        void Remove(int id);
        void update(T item);
        T FindBy(int id);
        IEnumerable<T> FindAll();
    }
}

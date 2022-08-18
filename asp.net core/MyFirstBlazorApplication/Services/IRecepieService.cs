using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcesss.Models;

namespace MyFirstBlazorApplication.Services
{
    public interface IRecepieService
    {
        Recepie Create(Recepie recipe);
        Recepie Get(int id);
        List<Recepie> List();
        Recepie Update(Recepie recipe);
        void Delete(int id);
    }
}

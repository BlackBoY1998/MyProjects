﻿using DataAcesss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstBlazorApplication.Services
{
    public class RecepieService : IRecepieService
    {
        public Recepie Create(Recepie recipe)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Recepie Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recepie> List()
        {

            var recipes = new List<Recepie>
            {
                //new Recepie
                //{
                //    Id = 1,
                //    Title = "Burger",
                //    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                //    DateCreated = DateTime.Now.AddDays(-2),
                //    DateUpdated = DateTime.Now.AddDays(-1)
                //},
                //new Recepie
                //{
                //    Id = 2,
                //    Title = "Pizza",
                //    Description = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.",
                //    DateCreated = DateTime.Now.AddDays(-4),
                //    DateUpdated = DateTime.Now.AddDays(-3)
                //},
                //new Recepie
                //{
                //    Id = 3,
                //    Title = "Lasagne",
                //    Description = "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.",
                //    DateCreated = DateTime.Now.AddDays(-6),
                //    DateUpdated = DateTime.Now.AddDays(-5)
                //},
            };

            return recipes;
        }

        public Recepie Update(Recepie recipe)
        {
            throw new NotImplementedException();
        }
    }
}

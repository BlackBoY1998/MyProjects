using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamleshtest
{
    class calc
    {
        public void add()
        {
            for (int i = 0; i <100; i++)
            {
                if (i % 3 == 0 && i%5==0) {
                    Console.WriteLine(i);
                }
               
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            calc c = new calc();
            c.add();
            Console.ReadLine();

        }
        
         
    }
}

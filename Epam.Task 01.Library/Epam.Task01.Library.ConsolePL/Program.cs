using AbstractValidation;
using Epam.Task01.Library.CollectionDAL;
using Epam.Task01.Library.Common;
using Epam.Task01.Library.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.ConsolePL
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //Menu menu = new Menu();
            //menu.Open();

            foreach (var item in DependencyResolver.CommonDao.GetLibraryItemsByYearOfPublishing())
            {
                //foreach (var item2 in item.Authors)
                //{
                //    Console.WriteLine("----" + item2.Id + item2.FirstName + item2.LastName);
                //}
                Console.WriteLine(item.YearOfPublishing);
                Console.WriteLine(item.Id);
                Console.WriteLine(item.GetType().Name);

                Console.WriteLine("-");
            }

            Console.ReadLine();
        }
    }
}

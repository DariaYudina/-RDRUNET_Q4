﻿using AbstractValidation;
using Epam.Task01.Library.Common;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.ConsolePL
{
    public class Menu
    {
        private static List<ValidationException> _validationResult;
        public void open()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Меню:" + Environment.NewLine +
                              "0. Выход" + Environment.NewLine +
                              "1. Добавленить запись в каталог " + Environment.NewLine +
                              "2. Удалить запись" + Environment.NewLine +
                              "3. Посмотреть каталог" + Environment.NewLine +
                              "4. Найти запись по названию" + Environment.NewLine +
                              "5. Сортировать по году выпуска в прямом порядке" + Environment.NewLine +
                              "6. Сортировать по году выпуска в обратном порядке" + Environment.NewLine +
                              "7. Найти книги автора (включая соавторство)" + Environment.NewLine +
                              "8. Найти все патенты данного изобретателя (включая соавторство)" + Environment.NewLine +
                              "9. Найти все книги и патенты данного автора (включая соавторство)" + Environment.NewLine +
                              "10.Вывод книг, название и издательство которых начинается с заданного набора символов, с группировкой по издательству" + Environment.NewLine +
                              "11.Группировка записей по годам издания" + Environment.NewLine +
                              "Введите выбранный пункт меню:"
                             );

                if (int.TryParse(Console.ReadLine(), out int selectedOption))
                {
                    switch (selectedOption)
                    {
                        case 0:
                            repeat = false;
                            break;
                        case 1:
                            AddCatalogItem();
                            break;
                        case 2:
                            
                            break;
                        case 3:
                            
                            break;
                        case 4:
                            
                            break;
                        case 5:
                            
                            break;
                        case 6:
                            
                            break;
                        case 7:
                            
                            break;
                        case 8:
                            
                            break;
                        case 9:
                            
                            break;
                        case 10:
                          
                            break;
                        case 11:
                            
                            break;
                        case 12:
                            
                            break;
                        default:
                            Console.WriteLine("----------------------------------------------------------------");
                            Console.WriteLine("Введите существующий пункт меню");
                            Console.WriteLine("----------------------------------------------------------------");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("Введите число");
                    Console.WriteLine("----------------------------------------------------------------");
                }
            } 
            while (repeat);
        }
        public void AddCatalogItem()                          
        {
            try
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("Выберите тип добавляемого объекта:");
                Console.WriteLine("Меню:" + Environment.NewLine +
                                "0. Выход" + Environment.NewLine +
                                "1. Книга " + Environment.NewLine +
                                "2. Патент" + Environment.NewLine +
                                "3. Газета" + Environment.NewLine +
                                "Введите выбранный пункт меню:"
                                );
                if (int.TryParse(Console.ReadLine(), out int selectedOption))
                {
                    switch (selectedOption)
                    {
                        case 0:
                            break;
                        case 1:
                            AddBook();
                            break;
                        case 2:
                            AddPatent();
                            break;
                        case 3:
                            AddNewspaper();
                            break;
                        default:
                            Console.WriteLine("----------------------------------------------------------------");
                            Console.WriteLine("Введите существующий пункт меню");
                            Console.WriteLine("----------------------------------------------------------------");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("Введите число");
                    Console.WriteLine("----------------------------------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine(e.Message);
                Console.WriteLine("----------------------------------------------------------------");
            }
        }
        private void AddBook() 
        {
            _validationResult = new List<ValidationException>();
            Book book1 = new Book(new List<Author>() { new Author("HHHhhh","Hahaha")}, "", "", 2019, "", 1, "fdsdsafdsdsdsdsdsdsdsdsdsds", 2, "");
            bool res = DependencyResolver.BookLogic.AddBook(_validationResult, book1);
            Console.WriteLine(res);

                foreach (var error in _validationResult)
                {
                    Console.WriteLine(error.Property + ": " + error.Message);
                }
        }
        private void AddPatent() {  }
        private void AddNewspaper() {  }
    }
}

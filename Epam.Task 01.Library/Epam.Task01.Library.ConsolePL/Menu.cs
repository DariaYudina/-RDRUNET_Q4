using AbstractValidation;
using Epam.Task01.Library.Common;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.ConsolePL
{
    public class Menu
    {
        private static List<ValidationException> _validationResult;

        public void Open()
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
                            DeleteCatalogItem();
                            break;
                        case 3:
                            GetAllCatalog();
                            break;
                        case 4:
                            SearchByTItle();
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
                            SearchBooksAndPatentsByAutors();
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
        public void GetAllCatalog()
        {
            IEnumerable<AbstractLibraryItem> catalog = DependencyResolver.CommonLogic.GetAllAbstractLibraryItems();
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog:");
            foreach (AbstractLibraryItem item in catalog)
            {
                Console.WriteLine("Catalog id: " + item.LibaryItemId + "| Title: " + item.Title);
            }
            Console.WriteLine("----------------------------------------------------------------");
        }
        public void DeleteCatalogItem()
        {
            try
            {
                Console.WriteLine("----------------------------------------------------------------");
                GetAllCatalog();
                Console.WriteLine("Выберите id удаляемного объекта:");
                if (int.TryParse(Console.ReadLine(), out int selectedOption))
                {
                    bool res = DependencyResolver.CommonLogic.DeleteLibraryItemById(selectedOption);
                    Console.WriteLine("Результат удаления: " + res);
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
            Book book1 = new Book(new List<Author>() { new Author("Петр", "Петров") }, "Саратов", "Москва", 2019, "ISBN 2-266-11156-6", "Title", 200, "да");
            bool res = DependencyResolver.BookLogic.AddBook(_validationResult, book1);
            Console.WriteLine("Результат валидации: " + res);
            foreach (ValidationException error in _validationResult)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
            var _validationResult2 = new List<ValidationException>();
            Book book2 = new Book(new List<Author>() { new Author("Петр", "Петров") }, "Ростов", "Эксмо", 2000, "ISBN 2-256-11756-6","Hi", 500, "да");
            bool res2 = DependencyResolver.BookLogic.AddBook(_validationResult2, book2);
            Console.WriteLine("Результат валидации: " + res);
            foreach (ValidationException error in _validationResult2)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
        }
        private void AddPatent()
        {
            _validationResult = new List<ValidationException>();
            Patent patent = new Patent(new List<Author>() { new Author("Петр", "Петров") } , "Россия", 12819 , new DateTime(2015, 7, 20), new DateTime(2016, 7, 21), "Крутой патент", 128, "",2018 );
            bool res = DependencyResolver.PatentLogic.AddPatent(_validationResult, patent);
            foreach (ValidationException error in _validationResult)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
        }
        private void AddNewspaper()
        {
        }
        private void SearchBooksAndPatentsByAutors()
        {
            //List<Author> authors = new List<Author>() { new Author("Петр", "Петров") };
            //string search = "Петр Петров";
            //IEnumerable<AbstractValidation> res = DependencyResolver.SearchLogic.GetBooksAndPatentsByAuthor(new Author("Петр", "Петров"));
            //IEnumerable<AbstractLibraryItem> abstractcollection = res.OfType<AbstractLibraryItem>();
            //foreach (AbstractLibraryItem item in abstractcollection)
            //{
            //    Console.WriteLine( item.LibaryItemId + " " + item.Title);
            //}
        }
        public void SearchByTItle()
        {
            string search = "Title";
            var res = DependencyResolver.CommonLogic.GetLibraryItemsByTitle(search);
            foreach (var item in res)
            {
                Console.WriteLine(item.LibaryItemId + " " + item.Title);
            }
        }
    }
}

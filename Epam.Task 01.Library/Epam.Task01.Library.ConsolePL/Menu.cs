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
        private static List<ValidationObject> _validationResult;
        private static IEnumerable<AbstractLibraryItem> _library;
        public void Open()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Menu:" + Environment.NewLine +
                              "0. Exit" + Environment.NewLine +
                              "1. Add item to catalog " + Environment.NewLine +
                              "2. Delete item from catalog" + Environment.NewLine +
                              "3. Get all catalog" + Environment.NewLine +
                              "4. Search item by title" + Environment.NewLine +
                              "5. Order by year" + Environment.NewLine +
                              "6. Order by year descending" + Environment.NewLine +
                              "7. Get books by author" + Environment.NewLine +
                              "8. Get patents by author" + Environment.NewLine +
                              "9. Get books and patents by author" + Environment.NewLine +
                              "10.Get by title with group by publishing company" + Environment.NewLine +
                              "11.Group by year of publishing and load" + Environment.NewLine +
                              "Enter the selected menu item:"
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
                            SortByYear();
                            break;
                        case 6:
                            SortByYearDesc();
                            break;
                        case 7:
                            SearchBooksByAutors();
                            break;
                        case 8:
                            SearchPatentsByAutors();
                            break;
                        case 9:
                            SearchBooksAndPatentsByAutors();
                            break;
                        case 10:
                            GetBooksGroupByPublishingCompanyWithTitle();
                            break;
                        case 11:
                            GetGroupLibraryItemByYearOfPublishing();
                            break;
                        default:
                            Console.WriteLine("----------------------------------------------------------------");
                            Console.WriteLine("Enter an existing menu item");
                            Console.WriteLine("----------------------------------------------------------------");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("Enter a number");
                    Console.WriteLine("----------------------------------------------------------------");
                }
            }
            while (repeat);
        }

        public void GetBooksGroupByPublishingCompanyWithTitle()
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog:");
            string title = "Title";
            foreach (var item in GroupBooksByPublishingCompany(title))
            {
                Console.WriteLine($"Year: {item.Key}");
                foreach (var i in item)
                {
                    Console.WriteLine($"\t{i.Title}");
                }
            }
            Console.WriteLine("----------------------------------------------------------------");
        }

        public void GetGroupLibraryItemByYearOfPublishing()
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog:");
            foreach (var item in GroupItemsByYearOfPublishing())
            {
                Console.WriteLine($"Year: {item.Key}");
                foreach (var i in item)
                {
                    Console.WriteLine($"\t{i.Title}");
                }
            }
            Console.WriteLine("----------------------------------------------------------------");
        }

        public void AddCatalogItem()
        {
            try
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("Select the type of item:");
                Console.WriteLine("Menu:" + Environment.NewLine +
                                "0. Exit" + Environment.NewLine +
                                "1. Book " + Environment.NewLine +
                                "2. Patent" + Environment.NewLine +
                                "3. Newspaper" + Environment.NewLine +
                                "Enter the selected menu item:"
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
                            Console.WriteLine("Enter an existing menu item");
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
            if(_library == null)
            {
                _library = DependencyResolver.CommonLogic.GetAllAbstractLibraryItems();
            }
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog:");
            foreach (AbstractLibraryItem item in _library)
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
                Console.WriteLine("Select id of the deleted object:");
                if (int.TryParse(Console.ReadLine(), out int selectedOption))
                {
                    bool res = DependencyResolver.CommonLogic.DeleteLibraryItemById(selectedOption);
                    Console.WriteLine("Result: " + res);
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("Enter a number");
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
            _validationResult = new List<ValidationObject>();
            var _validationResult2 = new List<ValidationObject>();
            Book book = new Book(new List<Author>() { new Author("Петр", "Пет") }, "Ростов-на-Дону", "Москва", 1996, null, "Title2", 200, "да");
            Book book2 = new Book(new List<Author>() { new Author("Петр", "Петров") }, "Саратов", "Москва", 1996, null, "Title", 200, "да");
            bool res = DependencyResolver.BookLogic.AddBook(_validationResult, book);
            var res2 = DependencyResolver.BookLogic.AddBook(_validationResult2, book2);
            Console.WriteLine("Result of validation: " + res);
            Console.WriteLine("Result of validation2 : " + res2);
            foreach (ValidationObject error in _validationResult)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
            foreach (ValidationObject error in _validationResult2)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
        }

        private void AddPatent()
        {
            _validationResult = new List<ValidationObject>();
            Patent patent = new Patent(new List<Author>() { new Author("Петр", "Петров") } ,"Россия", 12819 , new DateTime(2015, 7, 20), new DateTime(1996, 7, 21), "Крутой патент", 128, "");
            bool res = DependencyResolver.PatentLogic.AddPatent(_validationResult, patent);
            foreach (ValidationObject error in _validationResult)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
        }

        private void AddNewspaper()
        {
            _validationResult = new List<ValidationObject>();
            Newspaper newspaper = new Newspaper( new Issue("ee", "Саратов", "ee" , "ISSN 1234-1234"), 1401, 1 , new DateTime(1401, 7, 20), 12, "ee" );
            bool res = DependencyResolver.NewspaperLogic.AddNewspaper(_validationResult, newspaper);
            Console.WriteLine(res);
            foreach (ValidationObject error in _validationResult)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
        }

        private void SearchBooksByAutors()
        {
            var res = DependencyResolver.CommonLogic.GetBooksByAuthor(new Author("Петр", "Петров"));
            foreach (AbstractLibraryItem item in res)
            {
                Console.WriteLine(item.LibaryItemId + " " + item.Title);
            }
        }

        private void SearchPatentsByAutors()
        {
            var res = DependencyResolver.CommonLogic.GetPatentsByAuthor(new Author("Петр", "Петров"));
            Console.WriteLine("----------------------------------------------------------------");
            foreach (AbstractLibraryItem item in res)
            {
                Console.WriteLine(item.LibaryItemId + " " + item.Title);
            }
            Console.WriteLine("----------------------------------------------------------------");
        }

        private void SearchBooksAndPatentsByAutors()
        {
            var res = DependencyResolver.CommonLogic.GetBooksAndPatentsByAuthor(new Author("Петр", "Петров"));
            Console.WriteLine("----------------------------------------------------------------");
            foreach (AbstractLibraryItem item in res)
            {
                Console.WriteLine(item.LibaryItemId + " " + item.Title);
            }
            Console.WriteLine("----------------------------------------------------------------");
        }

        public void SortByYear()
        {
            Console.WriteLine("----------------------------------------------------------------");
            _library = DependencyResolver.CommonLogic.SortByYear();
            Console.WriteLine("Catalog sorted");
            Console.WriteLine("----------------------------------------------------------------");
        }

        public void SortByYearDesc()
        {
            _library = DependencyResolver.CommonLogic.SortByYearDesc();
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog sorted");
            Console.WriteLine("----------------------------------------------------------------");
        }

        public void SearchByTItle()
        {
            string search = "Title";
            var res = DependencyResolver.CommonLogic.GetLibraryItemsByTitle(search);
            Console.WriteLine("----------------------------------------------------------------");
            foreach (var item in res)
            {
                Console.WriteLine(item.LibaryItemId + " " + item.Title);
            }
            Console.WriteLine("----------------------------------------------------------------");
        }

        private IEnumerable<IGrouping<int, AbstractLibraryItem>> GroupItemsByYearOfPublishing()
        {
            return DependencyResolver.CommonLogic.GetLibraryItemsByYearOfPublishing();
        }

        private IEnumerable<IGrouping<string, Book>> GroupBooksByPublishingCompany(string title)
        {
            return DependencyResolver.BookLogic.GetBooksByPublishingCompany(title);
        }
    }
}

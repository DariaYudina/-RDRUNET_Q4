using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AbstractValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Common;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.ConsolePL
{
    public class Menu
    {
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
                            GetBooksGroupByPublishingCompanyByPublishingCompany();
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

        public void GetBooksGroupByPublishingCompanyByPublishingCompany()
        {
            Console.WriteLine("Enter title:");
            string title = Console.ReadLine();
            IEnumerable<IGrouping<string, Book>> res = GroupBooksByPublishingCompany(title);
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog:");
            foreach (IGrouping<string, Book> item in res)
            {
                Console.WriteLine($"Publishing company: {item.Key}");
                foreach (Book i in item)
                {
                    Console.WriteLine($"\tid:{i.Id}| Название: {i.Title} isbn:{i.Isbn}");
                }
            }

            Console.WriteLine("----------------------------------------------------------------");
        }

        public void GetGroupLibraryItemByYearOfPublishing()
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog:");
            foreach (IGrouping<int, AbstractLibraryItem> item in GroupItemsByYearOfPublishing())
            {
                Console.WriteLine($"Year: {item.Key}");
                foreach (AbstractLibraryItem i in item)
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
            if (_library == null)
            {
                _library = DependencyResolver.CommonLogic.GetLibraryItems();
            }

            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Catalog:");
            foreach (AbstractLibraryItem item in _library)
            {
                Console.WriteLine("Catalog id: " + item.Id + "| Title: " + item.Title);
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
            Console.WriteLine("Enter title:");
            string search = Console.ReadLine();
            IEnumerable<AbstractLibraryItem> res = DependencyResolver.CommonLogic.GetLibraryItemsByTitle(search);
            Console.WriteLine("----------------------------------------------------------------");
            foreach (AbstractLibraryItem item in res)
            {
                Console.WriteLine(item.Id + " " + item.Title);
            }

            Console.WriteLine("----------------------------------------------------------------");
        }

        private void AddBook()
        {
            bool repeat = true;
            _validationObject = new ValidationObject();
            List<Author> authors = new List<Author>();
            Console.WriteLine("Adding a author: ");
            Console.WriteLine("Enter author name: ");
            string authorName = Console.ReadLine();
            Console.WriteLine("Enter author lastname: ");
            string authorLastname = Console.ReadLine();
            Author author = new Author(authorName, authorLastname);
            authors.Add(author);
            do
            {
                Console.WriteLine("Add more author?:" + Environment.NewLine +
                                "0. No" + Environment.NewLine +
                                "1. Yes " + Environment.NewLine +
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
                            Console.WriteLine("Enter author name: ");
                            authorName = Console.ReadLine();
                            Console.WriteLine("Enter author lastname: ");
                            authorLastname = Console.ReadLine();
                            author = new Author(authorName, authorLastname);
                            authors.Add(author);
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

            Console.WriteLine("Enter book title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter the place of publication (city) of the book: ");
            string city = Console.ReadLine();
            Console.WriteLine("Enter publishing company of book: ");
            string publishingCompany = Console.ReadLine();
            Console.WriteLine("Enter book year of publication : ");

            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("Year must be a number");
                Console.WriteLine("----------------------------------------------------------------");
                return;
            }

            Console.WriteLine("Entr book ISBN: ");
            string isbn = Console.ReadLine();
            Console.WriteLine("Enter book page count: ");

            if (!int.TryParse(Console.ReadLine(), out int pagecount))
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("Pagecount must be a number");
                Console.WriteLine("----------------------------------------------------------------");
                return;
            }

            Console.WriteLine("Enter book commentary: ");
            string commentary = Console.ReadLine();

            Book book = new Book(authors, city, publishingCompany, year, isbn, title, pagecount, commentary);
            bool validationRes = DependencyResolver.BookLogic.AddBook(out _validationObject, book);

            Console.WriteLine("Result of validation: " + validationRes);
            foreach (ValidationException error in _validationObject.ValidationExceptions)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
        }

        private void AddPatent()
        {
            bool repeat = true;
            _validationObject = new ValidationObject();
            List<Author> authors = new List<Author>();
            Console.WriteLine("Adding a author: ");
            Console.WriteLine("Enter author name: ");
            string authorName = Console.ReadLine();
            Console.WriteLine("Enter author lastname: ");
            string authorLastname = Console.ReadLine();
            Author author = new Author(authorName, authorLastname);
            authors.Add(author);
            do
            {
                Console.WriteLine("Add more author?:" + Environment.NewLine +
                                "0. No" + Environment.NewLine +
                                "1. Yes " + Environment.NewLine +
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
                            Console.WriteLine("Enter author name: ");
                            authorName = Console.ReadLine();
                            Console.WriteLine("Enter author lastname: ");
                            authorLastname = Console.ReadLine();
                            author = new Author(authorName, authorLastname);
                            authors.Add(author);
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
            string title = Console.ReadLine();
            string counry = Console.ReadLine();
            Console.WriteLine("Enter patent registration number:");
            string registrationNumber = Console.ReadLine();
            Console.WriteLine("Enter patent application date:");
            string datexample = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            Console.WriteLine($"{Environment.NewLine} Please specify a date. Format: " + datexample);
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime applicationDate))
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("applicationDate must be a date");
                Console.WriteLine("----------------------------------------------------------------");
                return;
            }

            Console.WriteLine("Enter patent publication date:");
            Console.WriteLine($"{Environment.NewLine} Please specify a date. Format: " + datexample);
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime publicationDate))
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("publicationDate must be a date");
                Console.WriteLine("----------------------------------------------------------------");
                return;
            }

            Console.WriteLine("Enter patent pagecount:");
            if (!int.TryParse(Console.ReadLine(), out int pagecount))
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("pagecount must be a number");
                Console.WriteLine("----------------------------------------------------------------");
                return;
            }

            string commentary = Console.ReadLine();
            Patent patent = new Patent(1, authors, counry, registrationNumber, applicationDate, publicationDate, title, pagecount, commentary);
            bool res = DependencyResolver.PatentLogic.AddPatent(out _validationObject, patent);
            
            foreach (ValidationException error in _validationObject.ValidationExceptions)
            {
                Console.WriteLine(error.Property + ": " + error.Message);
            }
        }

        private void AddNewspaper()
        {
            Newspaper _newspaper = null;
            Console.WriteLine("Options:" + Environment.NewLine +
                "1. Create new issue " + Environment.NewLine +
                "2. Select exist issue" + Environment.NewLine +
                "Enter the selected menu item:"
                );
            if (int.TryParse(Console.ReadLine(), out int selectedOption))
            {
                switch (selectedOption)
                {
                    case 1:
                        {
                            if (!CteateNewNewspaper(out Newspaper newspaper))
                            {
                                Console.WriteLine("Newspaper not created");
                            }

                            _newspaper = newspaper;
                            break;
                        }

                    case 2:
                        {
                            SelectExistingIssue(out Newspaper newspaper);
                            _newspaper = newspaper;
                            break;
                        }

                    default:
                        Console.WriteLine("----------------------------------------------------------------");
                        Console.WriteLine("Enter an existing menu item");
                        Console.WriteLine("----------------------------------------------------------------");
                        break;
                }
            }

            if (_newspaper != null)
            {
                _validationObject = new ValidationObject();
                string datexample = DateTime.Now.ToString(CultureInfo.CurrentCulture);

                Console.WriteLine("Enter patent yearOfPublishing:");
                if (!int.TryParse(Console.ReadLine(), out int yearOfPublishing))
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("yearOfPublishing must be a number");
                    Console.WriteLine("----------------------------------------------------------------");
                    return;
                }

                Console.WriteLine("Enter patent countOfPublishing:");
                if (!int.TryParse(Console.ReadLine(), out int countOfPublishing))
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("countOfPublishing must be a number");
                    Console.WriteLine("----------------------------------------------------------------");
                    return;
                }

                Console.WriteLine("Enter patent dateOfPublishing:");
                Console.WriteLine($"{Environment.NewLine} Please specify a date. Format: " + datexample);
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfPublishing))
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("dateOfPublishing must be a date");
                    Console.WriteLine("----------------------------------------------------------------");
                    return;
                }

                Console.WriteLine("Enter patent pagecount:");
                if (!int.TryParse(Console.ReadLine(), out int pagecount))
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("pagecount must be a number");
                    Console.WriteLine("----------------------------------------------------------------");
                    return;
                }

                Console.WriteLine("Enter patent commentary:");
                string commentary = Console.ReadLine();

                Issue newspaper = new Issue(1, _newspaper, yearOfPublishing, countOfPublishing, dateOfPublishing, pagecount, commentary);
                bool res = DependencyResolver.IssueLogic.AddIssue(out _validationObject, newspaper);
                Console.WriteLine(res);
                foreach (ValidationException error in _validationObject.ValidationExceptions)
                {
                    Console.WriteLine(error.Property + ": " + error.Message);
                }
            }
            else
            {
                Console.WriteLine("Issue must bu not null or empty");
            }
        }

        private static ValidationObject _validationObject;

        private static IEnumerable<AbstractLibraryItem> _library;

        private bool CteateNewNewspaper(out Newspaper newspaper)
        {
            _validationObject = new ValidationObject();
            Console.WriteLine("Enter patent title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter patent city: ");
            string city = Console.ReadLine();
            Console.WriteLine("Enter patent publishingcompany: ");
            string publishingcompany = Console.ReadLine();
            Console.WriteLine("Enter patent issn:");
            string issn = Console.ReadLine();
            newspaper = new Newspaper(title, city, publishingcompany, issn);

            if (!DependencyResolver.NewspaperLogic.AddNewspaper(out _validationObject, newspaper))
            {
                foreach (ValidationException error in _validationObject.ValidationExceptions)
                {
                    Console.WriteLine(error.Property + ": " + error.Message);
                }

                return false;
            }

            return true;
        }

        private void SelectExistingIssue(out Newspaper issue)
        {
            issue = null;
            Console.WriteLine("Issues: ");
            foreach (var item in DependencyResolver.NewspaperLogic.GetNewspapers())
            {
                Console.WriteLine($"{item.Id}. | Title: {item.Title} | City: {item.City} | Publishing company: {item.PublishingCompany}");
            }

            Console.WriteLine("Select menu item number:");
            if (!int.TryParse(Console.ReadLine(), out int selectedoption))
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("pagecount must be a number");
                Console.WriteLine("----------------------------------------------------------------");
                return;
            }

            issue = DependencyResolver.NewspaperLogic.GetNewspaperById(selectedoption);
        }

        private void SearchBooksByAutors()
        {
            // Console.WriteLine("Enter author name and lastname");
            // string search = Console.ReadLine();
            // string[] author = search.Split(' ');
            // var res = DependencyResolver.CommonLogic.GetBooksByAuthor(new Author(author[0], author[1]));
            // foreach (AbstractLibraryItem item in res)
            // {
            // Console.WriteLine(item.Id + " " + item.Title);
            // }
        }

        private void SearchPatentsByAutors()
        {
            Console.WriteLine("Enter author name and lastname");
            string search = Console.ReadLine();
            string[] author = search.Split(' ');
            IEnumerable<Patent> res = DependencyResolver.PatentLogic.GetPatentsByAuthor(new Author(author[0], author[1]));
            Console.WriteLine("----------------------------------------------------------------");

            foreach (AbstractLibraryItem item in res)
            {
                Console.WriteLine(item.Id + " " + item.Title);
            }

            Console.WriteLine("----------------------------------------------------------------");
        }

        private void SearchBooksAndPatentsByAutors()
        {
            Console.WriteLine("Enter author name and lastname");
            string search = Console.ReadLine();
            string[] author = search.Split(' ');
            IEnumerable<AbstractLibraryItem> res = DependencyResolver.CommonLogic.GetBooksAndPatentsByAuthor(new Author(author[0], author[1]));
            Console.WriteLine("----------------------------------------------------------------");
            
            foreach (AbstractLibraryItem item in res)
            {
                Console.WriteLine(item.Id + " " + item.Title);
            }

            Console.WriteLine("----------------------------------------------------------------");
        }

        private IEnumerable<IGrouping<int, AbstractLibraryItem>> GroupItemsByYearOfPublishing()
        {
            return DependencyResolver.CommonLogic.GetLibraryItemsByYearOfPublishing();
        }

        private IEnumerable<IGrouping<string, Book>> GroupBooksByPublishingCompany(string publishingCompany)
        {
            return DependencyResolver.BookLogic.GetBooksByPublishingCompany(publishingCompany);
        }
    }
}

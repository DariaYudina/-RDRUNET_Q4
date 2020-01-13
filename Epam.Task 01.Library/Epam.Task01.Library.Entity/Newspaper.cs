﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Newspaper : AbstractLibraryItem
    {
        public string City { get; set; }
        public string PublishingCompany { get; set; }
        public int YearOfPublishing { get; set; }
        public int CountOfPublishing { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string ISSN { get; set; }
        public Newspaper(string city, string publishingCompany, int yearOfPublishing, int countOfPublishing, DateTime dateOfPublishing, string issn,
                        int libaryItemId, string title, int pageCount, string commentary)
                        : base(libaryItemId, title, pageCount, commentary)
        {
            City = city;
            PublishingCompany = publishingCompany;
            YearOfPublishing = yearOfPublishing;
            CountOfPublishing = countOfPublishing;
            DateOfPublishing = dateOfPublishing;
            ISSN = issn;
        }
    }
}
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class CreateBookModel
    {
        [Remote(
            "IsUniqueBook",
            "Book",
            AdditionalFields = "YearOfPublishing, Isbn, AuthorsId",
            ErrorMessage = "Such a Book already exists. Please enter a different product Title, Year of publishing," +
            "Authors or ISBN."
        )]
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 300 characters")]
        public string Title { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int PagesCount { get; set; }

        [StringLength(2000)]
        public string Commentary { get; set; } = "";

        [Required]
        [Range(1400, 2020, ErrorMessage = "Invalid year")]
        public int YearOfPublishing { get; set; }

        [Required]
        [RegularExpression(@"^((([A-Z][a-z]+)((\s([A-Z][a-z]+|[a-z]+))*)((-[a-z]+)?)((-([A-Z][a-z]+))?))|(([А-Я][а-я]+)((\s([А-Я][а-я]+|[а-я]+))*)((-[а-я]+)?)((-([А-Я][а-я]+))?)))$")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 200 characters")]
        public string City { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 300 characters")]
        public string PublishingCompany { get; set; }

        [RegularExpression(@"^(ISBN\s(([0-7])|(8\d|9[0-4])|(9([5-8]\d)|(9[0-3]))|(99[4-8][0-9])|(999[0-9][0-9]))-\d{1,7}-\d{1,7}-([0-9]|X))$")]
        [StringLength(19, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 19 characters")]
        public string Isbn { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();

        [ScaffoldColumn(false)]
        [Display(Name = "Authors")]
        public List<int> AuthorsId { get; set; } = new List<int>();
    }
}
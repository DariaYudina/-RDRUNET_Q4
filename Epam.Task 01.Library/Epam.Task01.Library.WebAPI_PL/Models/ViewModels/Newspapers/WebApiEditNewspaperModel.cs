using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Newspapers
{
    public class WebApiEditNewspaperModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 300 characters")]
        [Remote(
            "IsUniqueNewspaper",
            "Newspaper",
            AdditionalFields = "Issn",
            ErrorMessage = "Such a Newspaper already exists. Uniqueness is determined by the combination of the attributes" +
            " “Title” and “ISSN” "
        )]
        public string Title { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 200 characters")]
        [RegularExpression(@"^((([A-Z][a-z]+)(\s(([A-Z]|[a-z])[a-z]+))*(-([A-Z][a-z]+))?)|(([А-Я][а-я]+)(\s(([А-Я]|[а-я])[а-я]+))*(-([А-Я][а-я]+))?))$")]
        public string City { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 300 characters")]
        public string PublishingCompany { get; set; }

        [Required]
        [RegularExpression(@"^(ISSN\s\d{4}-\d{4})$")]
        [StringLength(14, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 14 characters")]
        public string Issn { get; set; }
    }
}
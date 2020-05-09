using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Patents
{
    public class WebApiCreatePatentModel
    {
        [Remote(
            "IsUniquePatent",
            "Patent",
            AdditionalFields = "RegistrationNumber, Country",
            ErrorMessage = "Such a Patent already exists. Uniqueness is determined by the combination of the attributes" +
            " “Country” and “Registration Number” "
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
        [RegularExpression(@"^(([A-Z][a-z]+)|([А-Я][а-я]+)|([A-Z]+|[А-Я]+))$")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 200 characters")]
        public string Country { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 9 characters")]
        public string RegistrationNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "not defined")]
        public DateTime? ApplicationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Required]
        public DateTime PublicationDate { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Authors")]
        public List<int> AuthorsId { get; set; } = new List<int>();
    }
}
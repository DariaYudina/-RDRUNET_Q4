using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Issues
{
    public class WebApiIssueWithoutNewspaperModel
    {
        public int Id { get; set; }

        public int PagesCount { get; set; }

        public string Commentary { get; set; }

        public int YearOfPublishing { get; set; }


        public int CountOfPublishing { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfPublishing { get; set; }
    }
}
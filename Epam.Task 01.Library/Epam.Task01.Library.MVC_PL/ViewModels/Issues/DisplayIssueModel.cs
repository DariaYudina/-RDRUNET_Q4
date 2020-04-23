using System;
using System.ComponentModel.DataAnnotations;

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class DisplayIssueModel
    {
        public int Id { get; set; }

        public int PagesCount { get; set; }

        public string Commentary { get; set; }

        public int YearOfPublishing { get; set; }

        public DisplayNewspaperModel Newspaper { get; set; }

        public int CountOfPublishing { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfPublishing { get; set; }
    }
}
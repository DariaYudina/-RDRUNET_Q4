using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.MVC_PL.ViewModels.Issues
{
    public class EditIssueModel
    {
        [Remote(
            "IsUniqueIssueEdit",
            "Issue",
            AdditionalFields = "Id, NewspaperId, DateOfPublishing",
            ErrorMessage = "Such a Issue already exists. Uniqueness is determined by the combination of the attributes" +
            " “Newspaper Title”, “Newspaper Publishing Company” and “Date of publishing”"
        )]
        [Required]
        [Range(0, int.MaxValue)]
        public int PagesCount { get; set; }

        [HiddenInput]
        public int Id { get; set; }

        [StringLength(2000)]
        public string Commentary { get; set; } = "";

        public int? CountOfPublishing { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of publishing")]
        public DateTime DateOfPublishing { get; set; }

        [HiddenInput]
        [Display(Name = "Newspaper")]
        public int NewspaperId { get; set; }

        [ScaffoldColumn(false)]
        public Newspaper Newspaper { get; set; }

        [ScaffoldColumn(false)]
        public SelectList NewspaperSelectList { get; set; }
    }
}
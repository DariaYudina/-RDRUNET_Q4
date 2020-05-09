using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Issues
{
    public class WebApiCreateIssueModel
    {
        [Remote(
           "IsUniqueIssue",
           "Issue",
           AdditionalFields = "NewspaperId, DateOfPublishing",
           ErrorMessage = "Such a Issue already exists. Uniqueness is determined by the combination of the attributes" +
           " “Newspaper Title”, “Newspaper Publishing Company” and “Date of publishing”"
       )]
        [Required]
        [Range(0, int.MaxValue)]
        public int PagesCount { get; set; }

        [StringLength(2000)]
        public string Commentary { get; set; } = "";

        public int? CountOfPublishing { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Date of publishing")]
        public DateTime DateOfPublishing { get; set; }

        [HiddenInput]
        [Display(Name = "Newspaper")]
        public int NewspaperId { get; set; }

        [ScaffoldColumn(false)]
        public Newspaper Newspaper { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Authors
{
    public class WebApiEditAuthorModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of the string should be from 1 to 200 characters")]
        public string LastName { get; set; }
    }
}
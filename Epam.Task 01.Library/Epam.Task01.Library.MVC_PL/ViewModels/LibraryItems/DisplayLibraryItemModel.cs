using System.ComponentModel.DataAnnotations;

namespace Epam.Task01.Library.MVC_PL.ViewModels
{
    public class DisplayLibraryItemModel
    {
        public string Title { get; set; }

        [DisplayFormat(NullDisplayText = "not defined")]
        public string Id { get; set; }

        public int PagesCount { get; set; }

        public int PrimaryKey { get; set; }

        public string Type { get; set; }

        public bool Deleted { get; set; }
    }
}
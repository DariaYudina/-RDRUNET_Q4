namespace Epam.Task01.Library.Entity
{
    public abstract class AbstractLibraryItem
    {
        public int LibaryItemId { get; set; }
        public string Title { get; set; }
        public int PagesCount { get; set; }
        public string Commentary { get; set; }
        public int YearOfPublishing { get; set; }
        public AbstractLibraryItem() { }
        public AbstractLibraryItem(string title, int pagesCount, string commentary)
        {
            Title = title;
            PagesCount = pagesCount;
            Commentary = commentary;
        }
    }
}

namespace Epam.Task01.Library.Entity
{
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Author() { }

        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Author(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}

namespace Epam.Task01.Library.MVC_PL.Models
{
    public class DisplayAuthorModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName[0]}. {LastName}";
        }
    }
}
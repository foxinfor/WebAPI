namespace BLL.DTO
{
    public class AuthorWithCountBooks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int BookCount { get; set; }
    }
}

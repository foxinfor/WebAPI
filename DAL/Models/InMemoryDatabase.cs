namespace DAL.Models
{
    public static class InMemoryDatabase
    {
        public static List<Author> Authors { get;} = new();
        public static List<Book> Books { get;} = new();
    }
}

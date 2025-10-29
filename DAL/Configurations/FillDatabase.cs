using DAL.Models;

namespace DAL.Configurations
{
    public class FillDatabase
    {
        public static void Fill(LibraryContext libraryContext)
        {
            if (!libraryContext.Authors.Any())
            {
                libraryContext.Authors.AddRange(
                    new Author { Name = "Isaac Asimov", DateOfBirth = new DateTime(1920, 1, 2) },
                    new Author { Name = "Arthur C. Clarke", DateOfBirth = new DateTime(1917, 12, 16) }
                );
            }

            if (!libraryContext.Books.Any())
            {
                libraryContext.Books.AddRange(
                    new Book { Title = "Foundation", AuthorId = 1 },
                    new Book { Title = "2001: A Space Odyssey", AuthorId = 2 }
                );
            }

            libraryContext.SaveChanges();
        }
    }
}

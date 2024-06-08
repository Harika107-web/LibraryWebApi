using LibraryWebApi.Common.Models;

namespace LibraryWebApi.Services.BookStore
{
    public class BookStoreService : IBookStoreService
    {
        public List<Book> GetBooks(int id)
        {
            var result = FetchBooks();
            result = result.Where(x => x.Id == id).ToList();
            return result;
        }

        private List<Book> FetchBooks()
        {
            List<Book> books = new List<Book>();
            books.Add(new Book()
            {
                Id = 1,
                Title = "Where the Crawdads Sing",
                Author = "Delia Owens",
                Description = "This novel tells the story of Kya Clark, the mysterious “Marsh Girl” of Barkley Cove, who grows up isolated in the wild North Carolina marshes. The story combines elements of a murder mystery with a coming-of-age narrative."
            });
            books.Add(new Book()
            {

                Id= 2,
                Title = "The Night Circus",
                Author = "Erin Morgenstern",
                Description = "A fantastical story set in a magical competition between two illusionists, The Night Circus tells the tale of a circus that appears without warning and only opens at night. The rivalry between the magicians evolves into an unexpected partnership amidst an enchanting backdrop."
            });
            return books;
        }
    }
}

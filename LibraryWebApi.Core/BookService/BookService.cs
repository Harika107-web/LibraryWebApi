using LibraryWebApi.Common.Models;

namespace LibraryWebApi.Core.BookService
{
  public class BookService : IBookService
  {
    private List<Book> _books;

    public BookService()
    {
      _books = new List<Book>();
            _books.Add(new Book()
            {
                Id = 1,
                Title = "Where the Crawdads Sing",
                Author = "Delia Owens",
                Description = "This novel tells the story of Kya Clark, the mysterious “Marsh Girl” of Barkley Cove, who grows up isolated in the wild North Carolina marshes. The story combines elements of a murder mystery with a coming-of-age narrative."
            });
            _books.Add(new Book()
            {

                Id = 2,
                Title = "The Night Circus",
                Author = "Erin Morgenstern",
                Description = "A fantastical story set in a magical competition between two illusionists, The Night Circus tells the tale of a circus that appears without warning and only opens at night. The rivalry between the magicians evolves into an unexpected partnership amidst an enchanting backdrop."
            });
        }

    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
      return Task.FromResult<IEnumerable<Book>>(_books);
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
      return await Task.Run(() => _books.First(b => b.Id == id));
    }

    public Task AddBookAsync(Book book)
    {
      book.Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
      _books.Add(book);
      return Task.CompletedTask;
    }

    public Task UpdateBookAsync(Book book)
    {
      var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
      if (existingBook != null)
      {
        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Year = book.Year;
      }
      else
      {
        throw new ArgumentException("Book not found");
      }

      return Task.CompletedTask;
    }

    public Task DeleteBookAsync(int id)
    {
      var bookToRemove = _books.FirstOrDefault(b => b.Id == id);
      if (bookToRemove != null)
      {
        _books.Remove(bookToRemove);
      }
      else
      {
        throw new ArgumentException("Book not found");
      }

      return Task.CompletedTask;
    }
  }
}

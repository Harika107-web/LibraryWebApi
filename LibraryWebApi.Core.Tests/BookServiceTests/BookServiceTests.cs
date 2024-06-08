using LibraryWebApi.Common.Models;
using LibraryWebApi.Core.BookService;

namespace LibraryWebApi.Core.Tests.BookServiceTests
{
  [TestClass]
  public class BookServiceTests
  {
    private IBookService _bookService;

    [TestInitialize]
    public void Setup()
    {
      _bookService = new BookService.BookService();
    }

    [TestMethod]
    public async Task GetAllBooksAsync_ShouldReturnAllBooks()
    {
      // Arrange
      await _bookService.AddBookAsync(new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 });
      await _bookService.AddBookAsync(new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2010 });

      // Act
      var result = await _bookService.GetAllBooksAsync();

      // Assert
      Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task GetBookByIdAsync_ShouldReturnCorrectBook()
    {
      // Arrange
      await _bookService.AddBookAsync(new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 });

      // Act
      var result = await _bookService.GetBookByIdAsync(1);

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual("Book 1", result.Title);
    }

    [TestMethod]
    public async Task AddBookAsync_ShouldIncreaseBookCount()
    {
      // Arrange
      var initialCount = (await _bookService.GetAllBooksAsync()).Count();
      var newBook = new Book { Id = 1, Title = "New Book", Author = "New Author", Year = 2020 };

      // Act
      await _bookService.AddBookAsync(newBook);

      // Assert
      var resultCount = (await _bookService.GetAllBooksAsync()).Count();
      Assert.AreEqual(initialCount + 1, resultCount);
    }

    [TestMethod]
    public async Task UpdateBookAsync_ShouldUpdateBook()
    {
      // Arrange
      var book = new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 };
      await _bookService.AddBookAsync(book);

      // Act
      book.Title = "Updated Book";
      await _bookService.UpdateBookAsync(book);
      var updatedBook = await _bookService.GetBookByIdAsync(1);

      // Assert
      Assert.IsNotNull(updatedBook);
      Assert.AreEqual("Updated Book", updatedBook.Title);
    }

    [TestMethod]
    public async Task DeleteBookAsync_ShouldDeleteBook()
    {
      // Arrange
      var book = new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 };
      await _bookService.AddBookAsync(book);

      // Act
      await _bookService.DeleteBookAsync(1);
      var deletedBook = await _bookService.GetBookByIdAsync(1);

      // Assert
      Assert.IsNull(deletedBook);
    }

    [TestMethod]
    public async Task GetAllBooksAsync_ShouldReturnEmptyListWhenNoBooks()
    {
      // Act
      var result = await _bookService.GetAllBooksAsync();

      // Assert
      Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public async Task GetBookByIdAsync_ShouldReturnNullForNonExistentBook()
    {
      // Act
      var result = await _bookService.GetBookByIdAsync(1);

      // Assert
      Assert.IsNull(result);
    }

    [TestMethod]
    public async Task UpdateBookAsync_ShouldThrowExceptionForNonExistentBook()
    {
      // Arrange
      var nonExistentBook = new Book { Id = 1, Title = "Non-existent Book", Author = "Author 1", Year = 2000 };

      // Act & Assert
      await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _bookService.UpdateBookAsync(nonExistentBook));
    }

    [TestMethod]
    public async Task DeleteBookAsync_ShouldThrowExceptionForNonExistentBook()
    {
      // Act & Assert
      await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _bookService.DeleteBookAsync(1));
    }
  }
}

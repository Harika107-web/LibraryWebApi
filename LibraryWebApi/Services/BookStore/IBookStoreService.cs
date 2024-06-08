using LibraryWebApi.Common.Models;

namespace LibraryWebApi.Services.BookStore;

public interface IBookStoreService
{
    List<Book> GetBooks(int id);
}
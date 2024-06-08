using LibraryWebApi.Common.Models;

namespace LibraryWebApi.Core.UserService
{
  public interface IUserService
  {
    Task<User> GetUser(string name);
    Task<User> AddUser(string name, int age);
    Task<IEnumerable<User>> GetAllUsers();
    Task UpdateUser(Guid uuid, string newName, int newAge);
    Task DeleteUser(Guid uuid);
  }
}

using LibraryWebApi.Common.Models;

namespace LibraryWebApi.Core.UserService
{
  public class UserService : IUserService
  {
    private readonly List<User> _dummyUsers = new List<User>();

    public UserService()
    {
      _dummyUsers.Add(new User() { Name = "Akash", Address = "Hyd", Age = 28, UUID = Guid.NewGuid() });
      _dummyUsers.Add(new User() { Name = "Krish", Address = "Ind", Age = 29, UUID = Guid.NewGuid() });
      _dummyUsers.Add(new User() { Name = "Sumit", Address = "Bpl", Age = 28, UUID = Guid.NewGuid() });
      _dummyUsers.Add(new User() { Name = "Vinay", Address = "Jbl", Age = 27, UUID = Guid.NewGuid() });

    }

    public async Task<User> GetUser(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException("name");
      }

      return await Task.Run(() => _dummyUsers.First(x => x.Name == name));
    }

    public async Task<User> AddUser(string name, int age)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException("name");
      }
      _dummyUsers.Add(new User() { Name = name, Age = age, UUID = Guid.NewGuid() });

      return await Task.Run(() => _dummyUsers.Last());
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
      return await Task.Run(() => _dummyUsers);
    }

    public async Task UpdateUser(Guid uuid, string newName, int newAge)
    {
      var userToUpdate = _dummyUsers.FirstOrDefault(u => u.UUID == uuid);
      if (userToUpdate != null)
      {
        userToUpdate.Name = newName;
        userToUpdate.Age = newAge;
      }
      else
      {
        throw new ArgumentException("User not found", nameof(uuid));
      }

      await Task.CompletedTask;
    }

    public async Task DeleteUser(Guid uuid)
    {
      var userToDelete = _dummyUsers.FirstOrDefault(u => u.UUID == uuid);
      if (userToDelete != null)
      {
        _dummyUsers.Remove(userToDelete);
      }
      else
      {
        throw new ArgumentException("User not found", nameof(uuid));
      }

      await Task.CompletedTask;
    }
  }
}

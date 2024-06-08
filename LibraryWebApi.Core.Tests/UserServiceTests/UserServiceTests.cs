using LibraryWebApi.Core.UserService;

namespace LibraryWebApi.Core.Tests.UserServiceTests
{
  [TestClass]
  public class UserServiceTests
  {
    private IUserService _userService;

    [TestInitialize]
    public void Setup()
    {
      _userService = new UserService.UserService();
    }

    [TestMethod]
    public async Task GetUser_ShouldReturnCorrectUser()
    {
      // Arrange
      var userName = "Akash";

      // Act
      var result = await _userService.GetUser(userName);

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(userName, result.Name);
    }

    [TestMethod]
    public async Task AddUser_ShouldIncreaseUserCount()
    {
      // Arrange
      var initialCount = (await _userService.GetAllUsers()).Count();
      var userName = "NewUser";
      var userAge = 30;

      // Act
      await _userService.AddUser(userName, userAge);

      // Assert
      var result = await _userService.GetAllUsers();
      Assert.AreEqual(initialCount + 1, result.Count());
      Assert.IsTrue(result.Any(u => u.Name == userName && u.Age == userAge));
    }

    [TestMethod]
    public async Task GetAllUsers_ShouldReturnAllUsers()
    {
      // Arrange & Act
      var result = await _userService.GetAllUsers();

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(4, result.Count()); // Assuming 4 initial users are added in the Setup
    }

    [TestMethod]
    public async Task UpdateUser_ShouldUpdateUserInformation()
    {
      // Arrange
      var userToUpdate = (await _userService.GetAllUsers()).First();
      var newName = "Ketan";
      var newAge = userToUpdate.Age + 1;

      // Act
      await _userService.UpdateUser(userToUpdate.UUID, newName, newAge);
      var updatedUser = (await _userService.GetAllUsers()).FirstOrDefault(u => u.UUID == userToUpdate.UUID);

      // Assert
      Assert.IsNotNull(updatedUser);
      Assert.AreEqual(newName, updatedUser.Name);
      Assert.AreEqual(newAge, updatedUser.Age);
    }

    [TestMethod]
    public async Task DeleteUser_ShouldDecreaseUserCount()
    {
      // Arrange
      var userToDelete = (await _userService.GetAllUsers()).First();
      var initialCount = (await _userService.GetAllUsers()).Count();

      // Act
      await _userService.DeleteUser(userToDelete.UUID);

      // Assert
      var result = await _userService.GetAllUsers();
      Assert.AreEqual(initialCount - 1, result.Count());
      Assert.IsFalse(result.Any(u => u.UUID == userToDelete.UUID));
    }

    [TestMethod]
    public async Task GetUser_ShouldThrowExceptionForNullOrEmptyName()
    {
      // Act & Assert
      await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _userService.GetUser(null));
      await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _userService.GetUser(string.Empty));
    }

    [TestMethod]
    public async Task AddUser_ShouldThrowExceptionForNullOrEmptyName()
    {
      // Act & Assert
      await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _userService.AddUser(null, 30));
      await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _userService.AddUser(string.Empty, 30));
    }

    [TestMethod]
    public async Task UpdateUser_ShouldThrowExceptionForNonExistentUser()
    {
      // Arrange
      var nonExistentUuid = Guid.NewGuid();

      // Act & Assert
      await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _userService.UpdateUser(nonExistentUuid, "NewName", 30));
    }

    [TestMethod]
    public async Task DeleteUser_ShouldThrowExceptionForNonExistentUser()
    {
      // Arrange
      var nonExistentUuid = Guid.NewGuid();

      // Act & Assert
      await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _userService.DeleteUser(nonExistentUuid));
    }
  }
}

using SmartTodo.Domain.Admins;
using TestCommon.TestConstants;
namespace SmartTodo.Domain.UnitTests.Admins;

public class AdminTests
{
    [Fact]
    public void RemoveTodo_WhenTodoDontBelongToAdmin_ShouldFail()
    {
        // Arrange
        // Create a admin
        // var admin = AdminFactory.Admins();  //no need for now the admin is HardCoded
        var admin = Constants.Admin;
        // Create todo
        var todo = TodoFactory.CreateTodo(id: Guid.NewGuid());
          

        // Act
        var removeTodoResults = admin.RemoveTodo(todo.Id);

        // Assert
        removeTodoResults.IsError.Should().BeTrue();
        removeTodoResults.FirstError.Should().Be(AdminErrors.TodoWithIdNotAssigned);
    }
}

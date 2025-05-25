using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DAL.Tests
{
    public class UserRepositoryUnitTests
    {
        [Fact]
        public void GetByUsername_ValidUsername_ReturnsUserWithMatchingUsername()
        {
            // Arrange
            var username = "john";
            var users = new List<User>
            {
                new User { Id = 1, Username = "john", Role = "citizen" },
                new User { Id = 2, Username = "alice", Role = "admin" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<NPPCSContext>(new DbContextOptions<NPPCSContext>());
            mockContext.Setup(c => c.Set<User>()).Returns(mockSet.Object);

            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = repository.GetByUsername(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
        }
    }
}

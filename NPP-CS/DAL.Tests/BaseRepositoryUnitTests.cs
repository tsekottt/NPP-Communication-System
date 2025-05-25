using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using DAL.EF;
using Xunit;

namespace DAL.Tests
{
    public class BaseRepositoryUnitTests
    {
        class TestUserRepository : BaseRepository<User>
        {
            public TestUserRepository(DbContext context) : base(context) { }
        }

        public class UserRepositoryTests
        {
            [Fact]
            public void Create_InputUserInstance_CallsAddMethodOfDbSetWithUser()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<NPPCSContext>().Options;
                var mockContext = new Mock<NPPCSContext>(options);
                var mockDbSet = new Mock<DbSet<User>>();
                mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

                var repository = new TestUserRepository(mockContext.Object);
                var expectedUser = new User { Id = 1, Username = "john" };

                // Act
                repository.Create(expectedUser);

                // Assert
                mockDbSet.Verify(dbSet => dbSet.Add(expectedUser), Times.Once());
            }

            [Fact]
            public void Delete_InputId_CallsFindAndRemoveMethodsWithCorrectArg()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<NPPCSContext>().Options;
                var mockContext = new Mock<NPPCSContext>(options);
                var mockDbSet = new Mock<DbSet<User>>();
                mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

                var repository = new TestUserRepository(mockContext.Object);
                var expectedUser = new User { Id = 1 };
                mockDbSet.Setup(m => m.Find(expectedUser.Id)).Returns(expectedUser);

                // Act
                repository.Delete(expectedUser.Id);

                // Assert
                mockDbSet.Verify(dbSet => dbSet.Find(expectedUser.Id), Times.Once());
                mockDbSet.Verify(dbSet => dbSet.Remove(expectedUser), Times.Once());
            }

            [Fact]
            public void Get_InputId_CallsFindMethodWithCorrectId()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<NPPCSContext>().Options;
                var mockContext = new Mock<NPPCSContext>(options);
                var mockDbSet = new Mock<DbSet<User>>();
                mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

                var expectedUser = new User { Id = 1 };
                mockDbSet.Setup(m => m.Find(expectedUser.Id)).Returns(expectedUser);

                var repository = new TestUserRepository(mockContext.Object);

                // Act
                var actualUser = repository.Get(expectedUser.Id);

                // Assert
                mockDbSet.Verify(dbSet => dbSet.Find(expectedUser.Id), Times.Once());
                Assert.Equal(expectedUser, actualUser);
            }
        }
    }
}

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
    public class RequestRepositoryUnitTests
    {
        [Fact]
        public void GetByUserId_ValidUserId_ReturnsOnlyRequestsOfUser()
        {
            // Arrange
            var userId = 1;
            var requests = new List<Request>
            {
                new Request { Id = 1, UserId = 1, Subject = "Test 1", Status = "new" },
                new Request { Id = 2, UserId = 2, Subject = "Test 2", Status = "new" },
                new Request { Id = 3, UserId = 1, Subject = "Test 3", Status = "resolved" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Request>>();
            mockSet.As<IQueryable<Request>>().Setup(m => m.Provider).Returns(requests.Provider);
            mockSet.As<IQueryable<Request>>().Setup(m => m.Expression).Returns(requests.Expression);
            mockSet.As<IQueryable<Request>>().Setup(m => m.ElementType).Returns(requests.ElementType);
            mockSet.As<IQueryable<Request>>().Setup(m => m.GetEnumerator()).Returns(requests.GetEnumerator());

            var mockContext = new Mock<NPPCSContext>(new DbContextOptions<NPPCSContext>());
            mockContext.Setup(c => c.Set<Request>()).Returns(mockSet.Object);

            var repository = new RequestRepository(mockContext.Object);

            // Act
            var result = repository.GetByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal(userId, r.UserId));
        }
    }
}

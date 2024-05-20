using Microsoft.AspNetCore.Mvc;
using Moq;
using Relacionamentos.Controllers;
using Relacionamentos.Data;
using Relacionamentos.Models;

namespace TestRelacionamentos
{
    [TestClass]
    public class PostControllerTests
    {
        [TestMethod]
        public async Task Index_ReturnsAViewResult_WithAListOfPosts()
        {
            // Arrange
            var mockContext = new Mock<RelacionamentosContext>();
            mockContext.Setup(repo => repo.Posts)
                       .Returns(GetTestPosts());
            var controller = new PostController(mockContext.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = Assert.IsAssignableFrom<IEnumerable<Post>>(
                viewResult.ViewData.Model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task Details_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Arrange
            var mockContext = new Mock<RelacionamentosContext>();
            var controller = new PostController(mockContext.Object);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_ReturnsNotFoundResult_WhenPostNotFound()
        {
            // Arrange
            var mockContext = new Mock<RelacionamentosContext>();
            mockContext.Setup(repo => repo.Posts.FindAsync(It.IsAny<int>()))
                       .ReturnsAsync((Post)null);
            var controller = new PostController(mockContext.Object);

            // Act
            var result = await controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Add more tests for other controller actions...

        private IQueryable<Post> GetTestPosts()
        {
            var posts = new List<Post>
            {
                new Post { Id = 1, Title = "Post 1", Content = "Content 1" },
                new Post { Id = 2, Title = "Post 2", Content = "Content 2" }
            }.AsQueryable();
            return posts;
        }
    }
}
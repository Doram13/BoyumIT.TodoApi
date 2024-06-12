using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTests
{
    public class TodoItemsControllerTests : IClassFixture<FunctionalTestFactory>
    {
        private readonly HttpClient _client;

        public TodoItemsControllerTests(FunctionalTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTodoItems_ReturnsSuccessStatusCode()
        {
            // Arrange
            var url = "/api/TodoItems";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}

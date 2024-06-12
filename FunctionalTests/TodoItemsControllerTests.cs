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
            // TODO: System.InvalidOperationException : Can't find 'C:\Users\USER\source\BoyumIt.TodoApi\FunctionalTests\bin\Debug\net8.0\testhost.deps.json'. This file is required for
            //  functional tests to run properly. There should be a copy of the file on your source project bin folder. If that is not the case, make sure that the property
            //  PreserveCompilationContext is set to true on your project file. E.g '<PreserveCompilationContext>true</PreserveCompilationContext>'.
            //  For functional tests to work they need to either run from the build output folder or the testhost.deps.json file from your application's output directory must be
            //  copied to the folder where the tests are running on. A common cause for this error is having shadow copying enabled when the tests run.

        }

    }
}

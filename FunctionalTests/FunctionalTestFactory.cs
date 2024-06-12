using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BoyumIT.TodoApi.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using BoyumIT.TodoApi.Services;

public class FunctionalTestFactory : WebApplicationFactory<Program> // New Test entry point needed instead of Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's DbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TodoListContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add DbContext using an in-memory database for testing.
            services.AddDbContext<TodoListContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
            services.AddScoped<ITodoItemService, TodoItemService>();
            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (TodoContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TodoListContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                // Seed the database with test data if necessary.
            }
        });
    }
}
// See https://aka.ms/new-console-template for more information

using GenericRepoDemo.DataAccess;
using GenericRepoDemo.Domain.Interfaces;
using GenericRepoDemo.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddDbContext<GenericRepoDemoDbContext>();
        services.AddTransient<IGenericRepository, EfCoreGenericRepository>();
        
        // add more repos per required db context
    })
    .Build();

await host.StartAsync();
await RunDemoAsync();

////////////////////////////////////////////////////////////////////////////////////

async Task RunDemoAsync()
{
    // Obviously, we would have an existing database, or seed one in a real implementation.
    // Since there's no data, these methods won't return anything right now,
    // but this should get the point across about how to use the repo.

    if (host.Services.GetRequiredService<IGenericRepository>() is not { } repo) return;
    
    // this is fine
    var orderById = await repo.GetByIdAsync<Order>(1);
    
    // this is fine
    var ordersByTag = await repo.GetListAsync<Order>(order => order.Tags.Contains("some_tag"));
    
    // this is fine
    var fred = await repo.GetFirstOrDefaultAsync<Customer>(customer => customer.Name == "Fred");

    // this is fine
    var areThereAnyProducts = await repo.AnyAsync<Product>(product => product.Sku == "ABC123");
    
    // this will not compile
    //var notADomainModel = await repo.GetAllAsync<NotADomainModel>();
    
    // Using the type constraint obviously won't prevent you from creating a domain model, but forgetting to add it to the relevant DbContext.
    // This check should be done in your infrastructure tests.
}
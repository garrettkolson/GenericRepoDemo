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
    var ordersByTag = await repo.GetListAsync<Order>(order => 
        order.Tags
            .Any(tag => tag.Description == "suspicious"));
    
    // this is fine
    var fred = await repo.GetFirstOrDefaultAsync<Customer>(customer => customer.Name == "Fred");

    // this is fine
    var areThereAnyProducts = await repo.AnyAsync<Product>(product => product.Sku == "ABC123");
    
    // due to the type constraints, this will not compile
    //var notADomainModel = await repo.GetAllAsync<NotADomainModel>();
    
    // but, using the projected versions, this will work
    var notADomainModel = await repo.GetTransformedFirstOrDefaultAsync<Product, NotADomainModel>(
        product => product.Id == 1,
        product => new NotADomainModel
        {
            Id = product.Id, 
            SomeProp = product.Sku
        });
    
    // or, if you only want some of the data without having to pull down everything first
    var names = await repo.GetTransformedListAsync<Customer, string>(
        customer => customer.Tags
            .Any(tag => tag.Description == "buys a lot"),
        customer => customer.Name);

    // Using the type constraint obviously won't prevent you from creating a domain model, and forgetting to add it to the relevant DbContext.
    // These types of checks should be done in your infrastructure tests.
}
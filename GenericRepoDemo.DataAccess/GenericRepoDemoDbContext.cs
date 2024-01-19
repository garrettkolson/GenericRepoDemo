using GenericRepoDemo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericRepoDemo.DataAccess;

public class GenericRepoDemoDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Customer> Customers { get; set; }
}
namespace GenericRepoDemo.Domain.Models;

public class Order : DomainModelBase
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public ICollection<string> Tags { get; set; } = new List<string>();
    
    // other stuff

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
namespace GenericRepoDemo.Domain.Models;

public class Order : DomainModelBase
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
    
    // other stuff
}
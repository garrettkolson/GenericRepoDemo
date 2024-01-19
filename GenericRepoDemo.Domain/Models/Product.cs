namespace GenericRepoDemo.Domain.Models;

public class Product : DomainModelBase
{
    public int Id { get; set; }

    public string Sku { get; set; }
    
    // other stuff
}
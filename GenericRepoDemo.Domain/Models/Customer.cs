namespace GenericRepoDemo.Domain.Models;

public class Customer : DomainModelBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Tag> Tags { get; set; }
}
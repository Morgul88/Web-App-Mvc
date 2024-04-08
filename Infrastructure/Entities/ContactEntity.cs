namespace Infrastructure.Entities;

public class ContactEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FullName { get; set; } = null!;

    public string EmailAdress { get; set; } = null!;

    public string Service { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}

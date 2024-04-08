using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class SubscriberEntity
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}

using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public string Size { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? CategoryId { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Category IdNavigation { get; set; } = null!;

    public virtual Order? Order { get; set; }
}

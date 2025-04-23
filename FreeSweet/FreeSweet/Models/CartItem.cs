using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? CartId { get; set; }

    public int? Quantity { get; set; }

    public double? TotalPrice { get; set; }

    public string? Size { get; set; }

    public double? Price { get; set; }

    public virtual Product Id1 { get; set; } = null!;

    public virtual Cart IdNavigation { get; set; } = null!;
}

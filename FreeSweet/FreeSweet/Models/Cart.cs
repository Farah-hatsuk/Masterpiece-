using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public int? TotalPrice { get; set; }

    public int? UsersId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User? Users { get; set; }
}

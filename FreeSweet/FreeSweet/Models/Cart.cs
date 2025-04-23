using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public int? TotalPrice { get; set; }

    public int? UsersId { get; set; }

    public virtual CartItem? CartItem { get; set; }

    public virtual User Id1 { get; set; } = null!;

    public virtual Product IdNavigation { get; set; } = null!;
}

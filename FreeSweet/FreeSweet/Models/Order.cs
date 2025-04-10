using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public int? Total { get; set; }

    public string? Note { get; set; }

    public string? Date { get; set; }

    public int? UsersId { get; set; }

    public int? ProdectId { get; set; }

    public virtual User Id1 { get; set; } = null!;

    public virtual Product IdNavigation { get; set; } = null!;

    public virtual Payment? Payment { get; set; }
}

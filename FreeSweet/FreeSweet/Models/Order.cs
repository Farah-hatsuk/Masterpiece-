using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public int? TotalAmount { get; set; }

    public string? Note { get; set; }

    public DateTime? Date { get; set; }

    public int? UsersId { get; set; }

    public string? Address { get; set; }

    public int? Phone { get; set; }

    public int? PaymentId { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment? Payment { get; set; }
}

using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public double? TotalAmount { get; set; }

    public string? Note { get; set; }

    public DateTime? Date { get; set; }

    public int? UsersId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public int? PaymentId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment? Payment { get; set; }

    public virtual User? Users { get; set; }
}

using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Payment
{
    public int Id { get; set; }

    public double? Total { get; set; }

    public string? PaymentMethoud { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

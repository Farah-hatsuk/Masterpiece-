using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public string Size { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Type { get; set; }

    public int CategoryId { get; set; }

    public string Img1 { get; set; } = null!;

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public string? Img4 { get; set; }

    public int Rating { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

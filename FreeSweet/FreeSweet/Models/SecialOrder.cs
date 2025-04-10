using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class SecialOrder
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Img { get; set; }

    public string? Date { get; set; }

    public string? Message { get; set; }
}

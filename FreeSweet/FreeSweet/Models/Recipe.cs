using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? PrepTime { get; set; }

    public string? CookTime { get; set; }

    public string? TotalTime { get; set; }

    public string? Ingredient { get; set; }

    public string? Instructions { get; set; }

    public string? Notes { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public string? Img4 { get; set; }

    public string? Img5 { get; set; }
}

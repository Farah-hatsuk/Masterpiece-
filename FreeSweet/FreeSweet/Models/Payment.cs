using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int? Total { get; set; }

    public int? OrdesId { get; set; }

    public virtual Order IdNavigation { get; set; } = null!;
}

﻿using System;
using System.Collections.Generic;

namespace FreeSweet.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Img { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? ConfirmPassword { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Order? Order { get; set; }
}

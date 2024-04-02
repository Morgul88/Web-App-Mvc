﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Model;

public class SignInModel
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}

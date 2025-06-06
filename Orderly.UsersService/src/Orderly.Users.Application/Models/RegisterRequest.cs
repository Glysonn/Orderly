﻿using Orderly.Users.Domain.Users;

namespace Orderly.Users.Application.Models;

public sealed record RegisterRequest(
    string Email,
    string Password,
    string Name,
    Gender Gender);

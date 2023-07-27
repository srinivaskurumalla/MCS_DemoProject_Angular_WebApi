using System;
using System.Collections.Generic;

namespace MCS_DemoProject_Angular_WebApi.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
    }
}

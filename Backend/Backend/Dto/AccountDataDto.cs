using Backend.Models;
using System;

namespace Backend.Dto
{
    public class AccountDataDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public AccountType AccountType { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ImageUrl { get; set; }

        public bool AccountVerified { get; set; }
    }
}

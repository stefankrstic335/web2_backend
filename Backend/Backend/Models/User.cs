using System;

namespace Backend.Models
{
    public enum AccountStatus
    {
        New,
        Verified,
        Blocked
    }

    public class User
    {
        public string Id { get; set; }  

        public string Name { get; set; }
        public string Lastname { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public AccountType AccountType { get; set; } 

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ImageUrl { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public bool IsSocialLogin { get; set; }


        public User() 
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}

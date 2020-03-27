using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorApp.Shared.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? Birthday { get; set; }

        public AccountStatus Status { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Avatar { get; set; }

        //Related tables
        public ICollection<UserRoles> UserRoles { get; set; }
        public ICollection<Message> Messages { get; set; }

        //Easy props
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public enum AccountStatus
        {
            Active,
            Pending,
            Disabled,
            Deactivated,
        }
    }
}

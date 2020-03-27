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

        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        public string Username { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? Birthday { get; set; }

        public AccountStatus Status { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        public string Avatar { get; set; }

        [Display(Name ="Blog Level")]
        public int LevelId { get; set; }
        public Level Level { get; set; }

        //Related tables
        public ICollection<UserRoles> UserRoles { get; set; }
        public ICollection<Message> MessagesTo { get; set; }
        public ICollection<Message> MessagesFrom { get; set; }

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

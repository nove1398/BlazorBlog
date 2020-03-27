using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorApp.Shared.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}

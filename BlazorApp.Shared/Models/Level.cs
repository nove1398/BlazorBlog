using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorApp.Shared.Models
{
    public class Level
    {
        [Key]
        public int LevelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PostThreshold { get; set; }
    }
}

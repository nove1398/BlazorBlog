using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorApp.Shared.Models
{
    public class Message
    {
       [Key]
       public int MessageId { get; set; }

        public DateTime SentAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public bool IsArchived { get; set; }

        public int UserFromId { get; set; }
        public User UserFrom { get; set; }

        public int UserToId { get; set; }
        public User UserTo { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }

    }
}

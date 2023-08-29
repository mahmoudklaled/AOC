using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models
{
    public class Chat
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReciveId { get; set; }
        public string? Message { get; set; }
        public string? PhotoPath { get; set; }
        public string? VedioPath { get; set; }
        public bool Read { get; set; } = false;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}

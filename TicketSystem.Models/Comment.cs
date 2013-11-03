using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class Comment
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        [Required]
        public int TicketId { get; set; }

        [ScaffoldColumn(false)]
        public virtual Ticket Ticket { get; set; }

        [ScaffoldColumn(false)]
        [Required]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

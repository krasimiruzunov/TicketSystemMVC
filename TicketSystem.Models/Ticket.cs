using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class Ticket
    {
        private ICollection<Comment> comments;

        public Ticket()
        {
            this.comments = new HashSet<Comment>();
            this.Priority = Priority.Medium;
        }

        [Key]
        public int Id { get; set; }
        
        public ApplicationUser Author { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public string ScreenshotUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}

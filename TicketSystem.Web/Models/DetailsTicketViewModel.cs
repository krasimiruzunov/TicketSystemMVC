using TicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketSystem.Web.Models
{
    public class DetailsTicketViewModel
    {
        public DetailsTicketViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public Priority Priority { get; set; }

        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }

        public int MyProperty { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketSystem.Web.Models
{
    public class AddCommentModel
    {
        public string Content { get; set; }

        public int TicketId { get; set; }
    }
}
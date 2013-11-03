using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketSystem.Web.Models
{
    public class SearchTicketViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }
    }
}
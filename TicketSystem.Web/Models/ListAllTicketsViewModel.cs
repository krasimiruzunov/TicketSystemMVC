using TicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketSystem.Web.Models
{
    public class ListAllTicketsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public Priority Priority { get; set; }

        public string PriorityAsString
        {
            get
            {
                return this.Priority.ToString();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketSystem.Models;

namespace TicketSystem.Web.Models
{
    public class AddTicketViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ShouldNotContainBug(ErrorMessage="Title cannot contains the word \"bug\"!")]
        public string Title { get; set; }

        [Required]
        public int Priority { get; set; }

        public string ScreenshotUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
}
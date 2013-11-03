using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketSystem.Models;
using TicketSystem.Web.Models;
using Microsoft.AspNet.Identity;

namespace TicketSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (this.HttpContext.Cache["HomePageTickets"] == null)
            {
                var tickets = this.Data.Tickets.All()
                    .OrderByDescending(x => x.Comments.Count)
                    .ThenBy(y => y.Id)
                    .Take(6)
                    .Select(t => new HomePageTicketViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        AuthorName = t.Author.UserName,
                        CategoryName = t.Category.Name,
                        NumberOfComments = t.Comments.Count
                    }).ToList();

                this.HttpContext.Cache.Add("HomePageTickets", tickets.ToList(), null, DateTime.Now.AddHours(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
            }

            return View(this.HttpContext.Cache["HomePageTickets"]);
        }
    }
}
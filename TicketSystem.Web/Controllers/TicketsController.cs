using TicketSystem.Models;
using TicketSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace TicketSystem.Web.Controllers
{
    public class TicketsController : BaseController
    {
        public ActionResult Details(int id)
        {
            var viewModel = this.Data.Tickets.All().Where(x => x.Id == id)
                .Select(x => new DetailsTicketViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    AuthorName = x.Author.UserName,
                    CategoryName = x.Category.Name,
                    Priority = x.Priority,
                    Description = x.Description,
                    ScreenshotUrl = x.ScreenshotUrl,
                    Comments = x.Comments.Select(c => new CommentViewModel
                    {
                        AuthorName = c.User.UserName,
                        Content = c.Content
                    }).ToList()
                }).FirstOrDefault();

            return View(viewModel);
        }
        public ActionResult AddTicket()
        {
            ViewBag.Categories = new SelectList(this.Data.Categories.All(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTicket(AddTicketViewModel ticketModel)
        {
            if (ModelState.IsValid)
            {
                var authorId = User.Identity.GetUserId();
                var author = this.Data.Users.All().FirstOrDefault(u => u.Id == authorId);
                Ticket ticket = new Ticket()
                {
                    Author = author,
                    CategoryId = ticketModel.CategoryId,
                    Priority = (Priority)ticketModel.Priority,
                    ScreenshotUrl = ticketModel.ScreenshotUrl,
                    Title = ticketModel.Title,
                    Description = ticketModel.Description
                };

                this.Data.Tickets.Add(ticket);
                author.Points++;
                this.Data.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Categories = new SelectList(this.Data.Categories.All(), "Id", "Name");
            return View(ticketModel);
        }

        public ActionResult ListAllTickets()
        {
            return View();
        }

        public JsonResult GetTickets([DataSourceRequest] DataSourceRequest request)
        {
            var tickets = this.Data.Tickets.All().Select(x => new ListAllTicketsViewModel
            {
                Id = x.Id,
                AuthorName = x.Author.UserName,
                CategoryName = x.Category.Name,
                Title = x.Title,
                Priority = x.Priority
            }).OrderBy(t => t.Id);

            return Json(tickets.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(AddCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                var comment = new Comment()
                {
                    TicketId = commentModel.TicketId,
                    Content = commentModel.Content,
                    UserId = userId
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var viewModel = new CommentViewModel { AuthorName = username, Content = commentModel.Content };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public JsonResult GetCategories()
        {
            var result = this.Data.Categories
                .All().Select(x => new CategoryViewModel
                {
                    Id =  x.Id,
                    Name = x.Name
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TicketsByCategory(int? category)
        {
            var tickets = this.Data.Tickets.All();
            if(category != null)
            {
                tickets = tickets.Where(t => t.CategoryId == category);
            }

            var result = tickets
                .Select(x => new ListAllTicketsViewModel
                {
                    Id = x.Id,
                    AuthorName = x.Author.UserName,
                    CategoryName = x.Category.Name,
                    Title = x.Title,
                    Priority = x.Priority
                }).OrderBy(t => t.Id);

            return View(result.ToList());
        }
	}
}
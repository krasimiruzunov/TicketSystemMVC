using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketSystem.Models;
using TicketSystem.Data;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using TicketSystem.Web.Models;

namespace TicketSystem.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CreateComment([DataSourceRequest] DataSourceRequest request, CommentViewModel comment)
        {
            if (comment != null && ModelState.IsValid)
            {
                var newComment = new Comment
                {
                    Content = comment.Content
                };

                this.Data.Comments.Add(newComment);
                this.Data.SaveChanges();
            }

            return Json(new[] { comment }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadComments([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.Data.Comments.All()
                                  .Select(c => new CommentViewModel
                                  {
                                      Id = c.Id,
                                      Content = c.Content,
                                      AuthorName = c.User.UserName,
                                      TicketTitle = c.Ticket.Title
                                  }).ToList();

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateComment([DataSourceRequest] DataSourceRequest request, CommentViewModel comment)
        {
            var existingComment = this.Data.Comments.All().FirstOrDefault(c => c.Id == comment.Id);

            if (comment != null && ModelState.IsValid)
            {
                existingComment.Content = comment.Content;
                this.Data.SaveChanges();
            }

            return Json((new[] { comment }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteComment([DataSourceRequest] DataSourceRequest request, Category comment)
        {
            var existingComment = this.Data.Comments.All().FirstOrDefault(c => c.Id == comment.Id);
            
            this.Data.Comments.Delete(existingComment);
            this.Data.SaveChanges();

            return Json(new[] { comment }, JsonRequestBehavior.AllowGet);
        }
    }
}
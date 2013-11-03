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
    [Authorize(Roles="Admin")]
    public class CategoriesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CreateCategory([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var newCategory = new Category
                    {
                        Name = category.Name
                    };

                this.Data.Categories.Add(newCategory);
                this.Data.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCategories([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.Data.Categories.All()
                                  .Select(c => new CategoryViewModel
                                  {
                                      Id = c.Id,
                                      Name = c.Name
                                  }).ToList();

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCategory([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            var existingCategory = this.Data.Categories.All().FirstOrDefault(c => c.Id == category.Id);

            if (category != null && ModelState.IsValid)
            {
                existingCategory.Name = category.Name;
                this.Data.SaveChanges();
            }

            return Json((new[] { category }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCategory([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            var existingCategory = this.Data.Categories.All().FirstOrDefault(c => c.Id == category.Id);

            foreach (var ticket in existingCategory.Tickets.ToList())
            {
                foreach (var comment in ticket.Comments.ToList())
                {
                    this.Data.Comments.Delete(comment);
                }

                this.Data.Tickets.Delete(ticket);
            }

            this.Data.Categories.Delete(existingCategory);
            this.Data.SaveChanges();

            return Json(new[] { category }, JsonRequestBehavior.AllowGet);
        }
    }
}

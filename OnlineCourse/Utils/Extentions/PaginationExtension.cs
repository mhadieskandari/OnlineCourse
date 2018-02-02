using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.Extentions
{
    public static class PaginationExtension
    {

        public static void AddTitle(this Controller controller, string title)
        {
            controller.ViewBag.Title = title;
        }
        public static IQueryable<T> AddPagination<T>(this Controller controller, IQueryable<T> model, int page = 1, int pageSize = 50)
        {
            var pagination = new Pagination(page, pageSize);
            //controller.ViewBag.Pagination.Page = pagination.Page;
            //controller.ViewBag.Pagination.PageSize = pagination.PageSize;
            controller.ViewBag.Pagination = pagination;
            var m = model.Skip(pagination.Skip).Take(pagination.Take).ToList();
            //controller.ViewBag.Pagination.NextPage = m.Count < pagination.PageSize;
            return m.AsQueryable();
        }
    }

    public class Pagination
    {
        public int Take { set; get; }
        public int Skip { set; get; }
        public int Page { set; get; }

        public int PageSize { set; get; }
        public Pagination(int? page, int pagesize = 50)
        {
            Take = PageSize = pagesize;
            Page = page == null || page <= 1 ? 0 : page.Value - 1;
            Skip = Page * PageSize;
            Page += 1;
        }
    }
}

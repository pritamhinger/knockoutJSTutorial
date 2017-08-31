using AutoMapper;
using BootstrapIntroduction.DAL;
using BootstrapIntroduction.Models;
using BootstrapIntroduction.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BootstrapIntroduction.Controllers.api
{
    public class AuthorsController : ApiController
    {
        private BookContext db = new BookContext();

        public ResultList<AuthorViewModel> Get([FromUri]QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            var authors = db.Authors.OrderBy(queryOptions.Sort).
            Skip(start).
            Take(queryOptions.PageSize);
            queryOptions.TotalPages =
            (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Author, AuthorViewModel>();
            });

            var mapper = config.CreateMapper();
            return new ResultList<AuthorViewModel>
            {
                QueryOptions = queryOptions,
                Results = mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList())
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

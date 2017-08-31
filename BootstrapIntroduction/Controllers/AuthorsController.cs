using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapIntroduction.DAL;
using BootstrapIntroduction.Models;
using System.Web.ModelBinding;
using BootstrapIntroduction.ViewModels;
using AutoMapper;

namespace BootstrapIntroduction.Controllers
{
    public class AuthorsController : Controller
    {
        private BookContext db = new BookContext();
        
        // GET: Authors
        public ActionResult Index([Form] QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;
            var authors = db.Authors.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize);
            queryOptions.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / queryOptions.PageSize);
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
            });

            var mapper = config.CreateMapper();

            //return View(mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList()));
            return View(new ResultList<AuthorViewModel>
            {
                QueryOptions = queryOptions,
                Results = mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList())
            });
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View("Form", new AuthorViewModel());
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AuthorViewModel, Author>();
                });

                var mapper = config.CreateMapper();
                db.Authors.Add(mapper.Map<AuthorViewModel, Author>(author));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
            });

            var mapper = config.CreateMapper();
            return View("Form", mapper.Map<Author, AuthorViewModel>(author));
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Biography")] AuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AuthorViewModel, Author>();
                });

                var mapper = config.CreateMapper();

                db.Entry(mapper.Map<AuthorViewModel, Author>(author)).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
            });

            var mapper = config.CreateMapper();
            return View(mapper.Map<Author, AuthorViewModel>(author));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
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

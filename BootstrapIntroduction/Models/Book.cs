using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapIntroduction.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string title { get; set; }
        public string Isbn { get; set; }
        public string Synopsis { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual Author Author { get; set; }
    }
}
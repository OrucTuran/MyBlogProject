﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public string? AppUserDescription { get; set; }
        public List<Article> Articles { get; set; }
        public List<Comment> Comments { get; set; }
        public bool isAuthor { get; set; }
    }
}

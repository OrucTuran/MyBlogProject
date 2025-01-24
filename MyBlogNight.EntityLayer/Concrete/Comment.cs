using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.EntityLayer.Concrete
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Commenter { get; set; }
        public string CommenterEmail { get; set; }
        public string CommentDetail { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public int AppUserId { get; set; } //bu yorumu kim yapti
        public AppUser AppUser { get; set; }
        public int ArticleId { get; set; }// yorum hangi makaleye yapildi
        public Article Article { get; set; }
        public int CategoryId { get; set; } // Makale kategorisi
        public Category Category { get; set; }
    }
}

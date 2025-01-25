using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlogNight.DtoLayer.Dtos.CommentDtos
{
    public class CommentViewDTO
    {
        public List<Comment> Comments { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public int ArticleId { get; set; }
    }
}

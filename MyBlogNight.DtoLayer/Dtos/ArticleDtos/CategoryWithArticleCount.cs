using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DtoLayer.Dtos.ArticleDtos
{
    public class CategoryWithArticleCount
    {
        public string CategoryName { get; set; }
        public int ArticleCount { get; set; }
    }
}

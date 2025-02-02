using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DtoLayer.Dtos.DashboardDtos
{
    public class ArticleCategoryCommentAuthorCountDTO
    {
        public int TotalArticleCount { get; set; }
        public int TotalCategoryCount { get; set; }
        public int TotalCommentCount { get; set; }
        public int TotalAuthorCount { get; set; }
    }
}

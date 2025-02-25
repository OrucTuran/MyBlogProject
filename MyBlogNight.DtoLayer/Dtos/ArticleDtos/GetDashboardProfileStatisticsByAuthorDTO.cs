using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DtoLayer.Dtos.ArticleDtos
{
    public class GetDashboardProfileStatisticsByAuthorDTO
    {
        public int TotalBlog {  get; set; }
        public int TotalComments {  get; set; }
        public int TotalViews {  get; set; }
    }
}

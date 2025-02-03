using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DtoLayer.Dtos.CommentDtos
{
    public class DashboardPopulerMembersDTO
    {
        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public int CommentCount { get; set; }
    }
}

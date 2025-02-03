using MyBlogNight.DtoLayer.Dtos.CommentDtos;
using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        public List<Comment> TGetCommentsByArticleId(int id);
        public List<Comment> TGetCommentsByAppUserId(int id);
        List<DashboardPopulerMembersDTO> TGetMostActiveUsers();
    }
}

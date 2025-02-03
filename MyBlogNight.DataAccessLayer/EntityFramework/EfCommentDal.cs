using Microsoft.EntityFrameworkCore;
using MyBlogNight.DataAccessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.DataAccessLayer.Repositories;
using MyBlogNight.DtoLayer.Dtos.CommentDtos;
using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DataAccessLayer.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public EfCommentDal(BlogContext context) : base(context)
        {
        }

        public List<Comment> GetCommentsByAppUserId(int id)
        {
            var context = new BlogContext();
            var values=context.Comments.Where(x=>x.AppUserId==id).ToList();
            return values;
        }

        public List<Comment> GetCommentsByArticleId(int id)//bir makaleye(article)gore yorum getirecek
        {
            var context = new BlogContext();
            var values = context.Comments.Where(x => x.ArticleId == id).Include(y => y.AppUser).ToList();
            return values;
        }

        public List<DashboardPopulerMembersDTO> GetMostActiveUsers()
        {
            using (var context = new BlogContext())
            {
                return context.Comments
                    .GroupBy(c => c.AppUserId)
                    .Select(group => new DashboardPopulerMembersDTO
                    {
                        AppUserId = group.Key,
                        UserName = context.Users.Where(u => u.Id == group.Key).Select(u => u.UserName).FirstOrDefault(),
                        CommentCount = group.Count()
                    })
                    .OrderByDescending(u => u.CommentCount)
                    .ToList();
            }
        }
    }
}
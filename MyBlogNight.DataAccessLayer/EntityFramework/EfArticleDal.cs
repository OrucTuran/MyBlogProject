using Microsoft.EntityFrameworkCore;
using MyBlogNight.DataAccessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.DataAccessLayer.Repositories;
using MyBlogNight.DtoLayer.Dtos.ArticleDtos;
using MyBlogNight.DtoLayer.Dtos.DashboardDtos;
using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>, IArticleDal
    {
        public EfArticleDal(BlogContext context) : base(context)
        {
        }

        public List<Article> ArticleListWithCategory()
        {
            var context = new BlogContext();
            var values = context.Articles.Include(x => x.Category).ToList();
            return values;
        }
        public List<Article> ArticleListWithCategoryAndAppUser()
        {
            var context = new BlogContext();
            var values = context.Articles.Include(x => x.Category).Include(y => y.AppUser).ToList();
            return values;
        }

        public Article ArticleListWithCategoryAndAppUserByArticleId(int id)
        {
            var context = new BlogContext();
            var values = context.Articles.Where(x => x.ArticleId == id).Include(y => y.Category).Include(z => z.AppUser).FirstOrDefault();
            values.ArticleViewCount += 1;
            context.SaveChanges();
            return values;
        }

        public void ArticleViewCountIncrease(int id)
        {
            var context = new BlogContext();
            var updatedValue = context.Articles.Find(id);
            updatedValue.ArticleViewCount += 1;
            context.SaveChanges();
        }

        public List<Article> GetArticlesByAppUserId(int id)
        {
            var context = new BlogContext();
            return context.Articles.Where(x => x.AppUserId == id).Include(x=>x.Category).ToList();
        }

        public async Task<List<Article>> GetArticlesByAppUserIdAsync(int id)
        {
            var context = new BlogContext();
            return await context.Articles.Where(x => x.AppUserId == id).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public List<Article> GetArticlesByViewCount()
        {
            var context = new BlogContext();

            // ArticleViewCount'a göre sıralama yapıyoruz (en çok görüntülenen makale önce gelecek)
            var sortedArticles = context.Articles
                .OrderByDescending(a => a.ArticleViewCount) // ArticleViewCount'a göre azalan sıralama
                .Take(4)
                .Include(a => a.Category) // İlgili kategoriyi de dahil ediyoruz
                .Include(a => a.AppUser)  // İlgili kullanıcıyı da dahil ediyoruz
                .ToList();

            return sortedArticles;
        }

        public Article GetArticleWithCategory(int id)
        {
            using (var context = new BlogContext())
            {
                return context.Articles
                              .Include(a => a.Category) // Category ilişkisini dahil ediyoruz
                              .FirstOrDefault(a => a.ArticleId == id);
            }
        }

        public List<CategoryWithArticleCount> GetCategoriesWithArticleCount()
        {
            var context = new BlogContext();

            // Kategorileri ve her kategorinin makale sayısını alıyoruz
            var categoryCounts = context.Categories
                .Select(c => new CategoryWithArticleCount
                {
                    CategoryName = c.CategoryName,
                    ArticleCount = c.Articles.Count
                })
                .OrderByDescending(c => c.ArticleCount)  // En çok makale bulunan kategori en üstte olacak
                .ToList();

            return categoryCounts;
        }

        public List<Article> GetRandomTwoTop5ViewedArticles()
        {
            var context = new BlogContext();

            //en cok goruntulenen ilk 5 makale
            var top5Articles = context.Articles
                .OrderByDescending(a => a.ArticleViewCount)
                //.Take(5)
                .Include(a => a.Category)
                .Include(b => b.AppUser)
                .Include(c => c.Comments)
                .ToList();

            //rastgele 2 makale
            var random = new Random();
            var randomTwoArticles = top5Articles
                .OrderBy(x => random.Next())
                .Take(2)
                .ToList();

            return randomTwoArticles;
        }
        public int GetTotalArticleViewCount()
        {
            var context = new BlogContext();
            return context.Articles.Sum(a => a.ArticleViewCount ?? 0);
        }

        public List<BlogCommentGraphDTO> GetBlogOverview()
        {
            var context = new BlogContext();

            return context.Articles.Select(x => new BlogCommentGraphDTO
            {
                BlogTitle = x.Title,
                CommentCount = x.Comments.Count
            }).ToList();
        }
        public List<BlogCommentGraphDTO> GetBlogOverviewByAuthor(int userId)
        {
            var context = new BlogContext();

            return context.Articles
                .Where(x => x.AppUserId == userId)
                .Select(x => new BlogCommentGraphDTO
                {
                    BlogTitle = x.Title,
                    CommentCount = x.Comments.Count
                }).ToList();
        }
        public GetDashboardProfileStatisticsByAuthorDTO GetDashboardProfileStatisticsByAuthor(int authorId)
        {
            var context = new BlogContext();

            var totalArticles = context.Articles
       .Where(x => x.AppUserId == authorId)
       .Count();

            var commentCounts = context.Articles
                .Where(x => x.AppUserId == authorId)
                .Select(x => x.Comments.Count())
                .ToList();

            var totalComments = commentCounts.Sum();

            var totalViews = context.Articles
                .Where(x => x.AppUserId == authorId)
                .Sum(x => x.ArticleViewCount ?? 0);

            return new GetDashboardProfileStatisticsByAuthorDTO
            {
                TotalBlog = totalArticles,
                TotalComments = totalComments,
                TotalViews = totalViews
            };
        }
    }
}

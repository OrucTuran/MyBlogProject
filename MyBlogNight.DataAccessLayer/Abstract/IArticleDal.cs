using MyBlogNight.DtoLayer.Dtos.ArticleDtos;
using MyBlogNight.DtoLayer.Dtos.DashboardDtos;
using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DataAccessLayer.Abstract
{
    public interface IArticleDal : IGenericDal<Article>
    {
        List<Article> ArticleListWithCategory();
        List<Article> ArticleListWithCategoryAndAppUser(); //bütün listeyi götürür.
        Article ArticleListWithCategoryAndAppUserByArticleId(int id); 
        void ArticleViewCountIncrease(int id);
        List<Article> GetArticlesByAppUserId(int id);
        public Task<List<Article>> GetArticlesByAppUserIdAsync(int id);
        public List<CategoryWithArticleCount> GetCategoriesWithArticleCount();
        public List<Article> GetArticlesByViewCount();
        public List<Article> GetRandomTwoTop5ViewedArticles();
        Article GetArticleWithCategory(int id);
        int GetTotalArticleViewCount();
        public List<BlogCommentGraphDTO> GetBlogOverview();
        public List<BlogCommentGraphDTO> GetBlogOverviewByAuthor(int userId);
        public GetDashboardProfileStatisticsByAuthorDTO GetDashboardProfileStatisticsByAuthor(int authorId);
    }
}

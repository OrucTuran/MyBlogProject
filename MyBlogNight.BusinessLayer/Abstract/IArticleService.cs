﻿using MyBlogNight.DtoLayer.Dtos.ArticleDtos;
using MyBlogNight.DtoLayer.Dtos.DashboardDtos;
using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.BusinessLayer.Abstract
{
    public interface IArticleService : IGenericService<Article>
    {
        public List<Article> TArticleListWithCategory();
        public List<Article> TArticleListWithCategoryAndAppUser();
        public Article TArticleListWithCategoryAndAppUserByArticleId(int id);
        public void TArticleViewCountIncrease(int id);
        public List<Article> TGetArticlesByAppUserId(int id);
        public Task<List<Article>> TGetArticlesByAppUserIdAsync(int id);
        public List<CategoryWithArticleCount> TGetCategoriesWithArticleCount();
        public List<Article> TGetArticlesByViewCount();
        public List<Article> TGetRandomTwoTop5ViewedArticles();
        public int TGetTotalArticleViewCount();
        public List<BlogCommentGraphDTO> TGetBlogOverview();
        public List<BlogCommentGraphDTO> TGetBlogOverviewByAuthor(int userId);
        public GetDashboardProfileStatisticsByAuthorDTO TGetDashboardProfileStatisticsByAuthor(int authorId);
    }
}

﻿using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Abstract;
using MyBlogNight.DtoLayer.Dtos.ArticleDtos;
using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.BusinessLayer.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDal _articleDal;
        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }
        public List<Article> TArticleListWithCategory()
        {
            return _articleDal.ArticleListWithCategory();
        }

        public List<Article> TArticleListWithCategoryAndAppUser()
        {
            return _articleDal.ArticleListWithCategoryAndAppUser();
        }

        public Article TArticleListWithCategoryAndAppUserByArticleId(int id)
        {
            return _articleDal.ArticleListWithCategoryAndAppUserByArticleId(id);
        }

        public void TArticleViewCountIncrease(int id)
        {
            _articleDal.ArticleViewCountIncrease(id);
        }

        public void TDelete(int id)
        {
            _articleDal.Delete(id);
        }
        public List<Article> TGetAll()
        {
            return _articleDal.GetAll();
        }
        public List<Article> TGetArticlesByAppUserId(int id)
        {
            return _articleDal.GetArticlesByAppUserId(id);
        }

        public List<Article> TGetArticlesByViewCount()
        {
            return _articleDal.GetArticlesByViewCount();
        }

        public Article TGetById(int id)
        {
            return _articleDal.GetById(id);
        }

        public List<CategoryWithArticleCount> TGetCategoriesWithArticleCount()
        {
           return _articleDal.GetCategoriesWithArticleCount();
        }

        public List<Article> TGetRandomTwoTop5ViewedArticles()
        {
           return _articleDal.GetRandomTwoTop5ViewedArticles();
        }

        public int TGetTotalArticleViewCount()
        {
            return _articleDal.GetTotalArticleViewCount();
        }

        public void TInsert(Article entity)
        {
            _articleDal.Insert(entity);
        }
        public void TUpdate(Article entity)
        {
            _articleDal.Update(entity);
        }
    }
}

using ConsoleApp1.Interface;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Service
{
    public class PriceManager : IPriceManager
    {
        private readonly List<Article>  articles;
        private readonly List<Promotion> promotions;

        public PriceManager(List<Article> articles, List<Promotion> promotions)
        {
            this.articles = articles;
            this.promotions = promotions;
        }

        public void AddArticle(string name, double price, DateTime insertionDateTime)
        {
            var existingArticle = articles.Exists(articles => articles.Name == name);
            if(existingArticle)
            {
                throw new ArgumentException("Article already exists");
            }

            var article = new Article
            {
                Name = name,
                Price = price,
                CreateDate = insertionDateTime
            };
            articles.Add(article);
        }

        public bool AddPromotion(string articleName, double discount, DateTime startDate, int durationDays)
        {
            try
            {
                var article = articles.SingleOrDefault(_ => _.Name == articleName);
                var promotion = new Promotion
                {
                    StartDate = startDate,
                    EndDate = startDate.AddDays(durationDays),
                    Discount = discount,
                    Article = article
                };

                promotions.Add(promotion);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public IEnumerable<string> GetArticleNames()
        {
            return articles.Select(a => a.Name).ToList();
        }

        public double GetPrice(string name, DateTime date)
        {
            var existingArticle = articles.SingleOrDefault(articles => articles.Name == name);
            if (existingArticle == null)
            {
                throw new Exception($"Article {name} doesn't exist");
            }

            var existingPromotions = promotions.Where(p => p.StartDate <= date && p.EndDate >= date);
            var maxDiscount = existingPromotions.Where(a => a.Article?.Name == name || a.IsChristmas || a.IsClearance).Max(_ => _.Discount);

            return existingArticle.Price - (maxDiscount / 100);
        }

        public void SetChristmasPeriod(DateTime startDate, DateTime endDate)
        {
            var promotion = new Promotion
            {
                StartDate = startDate,
                EndDate = endDate,
                Discount = 30,
                IsChristmas = true,
            };
            promotions.Add(promotion);
        }

        public void SetClearancePeriod(DateTime startDate, int DaysDuration)
        {
            var promotion = new Promotion
            {
                StartDate = startDate,
                EndDate = startDate.AddDays(DaysDuration),
                Discount = 50,
                IsChristmas = true,
            };
            promotions.Add(promotion);
        }
    }
}

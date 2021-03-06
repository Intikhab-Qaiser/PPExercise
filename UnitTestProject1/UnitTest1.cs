using ConsoleApp1.Interface;
using ConsoleApp1.Models;
using ConsoleApp1.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class PriceManagerTests
    {
        private List<Article> articles;
        private List<Promotion> promotions;
        private IPriceManager priceManager;

        [TestInitialize]
        public void TestInitialize()
        {
            SetupTestData();
        }

        [TestMethod]
        public void GetPrice_when_regular_discount_returns_discounted_price()
        {
            // Arrange
            priceManager = new PriceManager(articles, promotions);
            var articleName = "Article-1";
            var date = new DateTime(2021,01,05);

            // Act
            var articlePrice = priceManager.GetPrice(articleName, date);

            // Asert
            Assert.AreEqual(4.4, articlePrice);
        }

        [TestMethod]
        public void GetPrice_when_christmis_discount_returns_discounted_price()
        {
            // Arrange
            priceManager = new PriceManager(articles, promotions);
            var articleName = "Article-1";
            var date = new DateTime(2020, 12, 10);

            // Act
            var articlePrice = priceManager.GetPrice(articleName, date);

            // Asert
            Assert.AreEqual(4.2, articlePrice);
        }

        [TestMethod]
        public void GetPrice_when_clearance_discount_returns_discounted_price()
        {
            // Arrange
            priceManager = new PriceManager(articles, promotions);
            var articleName = "Article-1";
            var date = new DateTime(2020, 7, 10);

            // Act
            var articlePrice = priceManager.GetPrice(articleName, date);

            // Asert
            Assert.AreEqual(4, articlePrice);
        }

        [TestMethod]
        public void GetPrice_when_no_article_returns_exception()
        {
            // Arrange
            priceManager = new PriceManager(articles, promotions);
            var articleName = "Article-5";
            var date = new DateTime(2021, 7, 10);

            // Act && Assert
            Assert.ThrowsException<Exception>(() => priceManager.GetPrice(articleName, date));
        }

        [TestMethod]
        public void AddArticle_when_already_exists_returns_argument_exception()
        {
            // Arrange
            priceManager = new PriceManager(articles, promotions);
            
            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => priceManager.AddArticle("Article-1", 5.6, DateTime.Now.Date));
        }

        private void SetupTestData()
        {
            articles = new List<Article>
            {
                new Article
                {
                    Name = "Article-1",
                    Price = 4.5,
                    CreateDate = new DateTime(2019,6,5)
                },
                new Article
                {
                    Name = "Article-2",
                    Price = 5.7,
                    CreateDate = new DateTime(2019,7,5)
                },
                new Article
                {
                    Name = "Article-3",
                    Price = 9.5,
                    CreateDate = new DateTime(2020,6,5)
                }
            };

            promotions = new List<Promotion>
            {
                new Promotion
                {
                    StartDate = new DateTime(2021,1,1),
                    EndDate = new DateTime(2021,1,10),
                    Discount = 10,
                    Article = articles[0]
                },
                new Promotion
                {
                    StartDate = new DateTime(2021,2,5),
                    EndDate = new DateTime(2021,2,15),
                    Discount = 5,
                    Article = articles[0]
                },
                new Promotion
                {
                    StartDate = new DateTime(2020,12,10),
                    EndDate = new DateTime(2020,12,31),
                    Discount = 30,
                    IsChristmas = true
                },
                 new Promotion
                {
                    StartDate = new DateTime(2019,12,10),
                    EndDate = new DateTime(2019,12,31),
                    Discount = 30,
                    IsChristmas = true
                },
                new Promotion
                {
                    StartDate = new DateTime(2020,6,10),
                    EndDate = new DateTime(2020,7,10),
                    Discount = 50,
                    IsClearance = true
                },
                 new Promotion
                {
                    StartDate = new DateTime(2019,6,10),
                    EndDate = new DateTime(2019,7,10),
                    Discount = 50,
                    IsClearance = true
                }
            };
        }
    }
}

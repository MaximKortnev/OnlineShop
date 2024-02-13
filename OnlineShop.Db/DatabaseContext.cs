﻿using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Приключения Буратино",
                    Author = "Алексей Толстой",
                    AboutTheBook = "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.",
                    AboutAuthor = "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.",
                    Quote = "Считаю до трёх, а потом как дам больно!",
                    Cost = (decimal)20.00,
                    Description = "Невероятная история о деревянном мальчике",
                    ImagePath = "image.jpg",
                    ImagePaths = ["image.jpg", "image1.jpg"]
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Алиса в стране чудес",
                    Author = "Льюис Кэрролл",
                    AboutTheBook = "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.",
                    AboutAuthor = "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.",
                    Quote = "Считаю до трёх, а потом как дам больно!",
                    Cost = (decimal)15.99,
                    Description = "Книга о приключениях девочки Асилы и о неведомых чудесах созданных Льюисом Кэрроллом ",
                    ImagePath = "image.jpg",
                    ImagePaths = ["image.jpg", "image1.jpg"]
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Марта",
                    Author = "Аполина Андреевна",
                    AboutTheBook = "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.",
                    AboutAuthor = "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.",
                    Quote = "Считаю до трёх, а потом как дам больно!",
                    Cost = (decimal)15.00,
                    Description = "Биографическая книга о мыслях и жизни девушки по имени Марта",
                    ImagePath = "image.jpg",
                    ImagePaths = ["image.jpg", "image1.jpg"]
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Жан Пьер Мюри. Автобиография",
                    Author = "Жан Пьер Мюри",
                    AboutTheBook = "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.",
                    AboutAuthor = "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.",
                    Quote = "Считаю до трёх, а потом как дам больно!",
                    Cost = (decimal)45.20,
                    Description = "Жан Пьер Мюри, одна из выдающихся личностей прошлого столетия",
                    ImagePath = "image.jpg",
                    ImagePaths = ["image.jpg", "image1.jpg"]
                });
        }
    }
}

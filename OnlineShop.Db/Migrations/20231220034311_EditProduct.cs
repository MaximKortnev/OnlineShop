using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class EditProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Orders_OrderId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Products_ProductId",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5778dfd6-362d-45e8-aeaf-75278d109b06"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("72a89d77-7676-4527-970f-46d6a4f28e0c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7ea1f35a-f68b-4e17-8a92-65c70c934040"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f36c10ca-f56f-4f8e-bcce-adbf7d0fc1c2"));

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_OrderId",
                table: "CartItems",
                newName: "IX_CartItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartId",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePaths",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AboutAuthor", "AboutTheBook", "Author", "Cost", "Description", "ImagePath", "ImagePaths", "Name", "Quote" },
                values: new object[,]
                {
                    { new Guid("5b7ff966-d260-4901-bf2f-e2c1465e77c3"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Алексей Толстой", 20m, "Невероятная история о деревянном мальчике", "image.jpg", null, "Приключения Буратино", "Считаю до трёх, а потом как дам больно!" },
                    { new Guid("68adbcb3-7a3f-43d2-85ae-080ad59e5d5d"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Жан Пьер Мюри", 45.2m, "Жан Пьер Мюри, одна из выдающихся личностей прошлого столетия", "image.jpg", null, "Жан Пьер Мюри. Автобиография", "Считаю до трёх, а потом как дам больно!" },
                    { new Guid("c90c959d-20c0-4a48-acb5-9230b1e9682c"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Льюис Кэрролл", 15.99m, "Книга о приключениях девочки Асилы и о неведомых чудесах созданных Льюисом Кэрроллом ", "image.jpg", null, "Алиса в стране чудес", "Считаю до трёх, а потом как дам больно!" },
                    { new Guid("d924cc47-ffbe-4f80-bdeb-4c9f1d3a33b3"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Аполина Андреевна", 15m, "Биографическая книга о мыслях и жизни девушки по имени Марта", "image.jpg", null, "Марта", "Считаю до трёх, а потом как дам больно!" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Orders_OrderId",
                table: "CartItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Orders_OrderId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5b7ff966-d260-4901-bf2f-e2c1465e77c3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("68adbcb3-7a3f-43d2-85ae-080ad59e5d5d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c90c959d-20c0-4a48-acb5-9230b1e9682c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d924cc47-ffbe-4f80-bdeb-4c9f1d3a33b3"));

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItem",
                newName: "IX_CartItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItem",
                newName: "IX_CartItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                table: "CartItem",
                newName: "IX_CartItem_CartId");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePaths",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "CartItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AboutAuthor", "AboutTheBook", "Author", "Cost", "Description", "ImagePath", "ImagePaths", "Name", "Quote" },
                values: new object[,]
                {
                    { new Guid("5778dfd6-362d-45e8-aeaf-75278d109b06"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Жан Пьер Мюри", 45.2m, "Жан Пьер Мюри, одна из выдающихся личностей прошлого столетия", "image.jpg", null, "Жан Пьер Мюри. Автобиография", "Считаю до трёх, а потом как дам больно!" },
                    { new Guid("72a89d77-7676-4527-970f-46d6a4f28e0c"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Льюис Кэрролл", 15.99m, "Книга о приключениях девочки Асилы и о неведомых чудесах созданных Льюисом Кэрроллом ", "image.jpg", null, "Алиса в стране чудес", "Считаю до трёх, а потом как дам больно!" },
                    { new Guid("7ea1f35a-f68b-4e17-8a92-65c70c934040"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Алексей Толстой", 20m, "Невероятная история о деревянном мальчике", "image.jpg", null, "Приключения Буратино", "Считаю до трёх, а потом как дам больно!" },
                    { new Guid("f36c10ca-f56f-4f8e-bcce-adbf7d0fc1c2"), "Толстой Алексей Николаевич – прозаик, поэт. Родился 29 декабря 1882 года в городе Николаевске Самарской губернии. После окончания реального училища в Самаре поступил на отделение механики Технологического института, но незадолго до окончания бросает его, окончательно решив посвятить себя литературному труду.", "Озорной мальчишка с длинным носом, Буратино, на протяжении 85 лет вновь и вновь зовёт детей отправиться в сказочное приключение. Вы побываете в Стране Дураков вместе с котом Базилио и лисой Алисой, погостите у прекрасной Мальвины, познакомитесь с черепахой Тортилой, перехитрите ужасного Карабаса Барабаса, и конечно же, раскроете тайну Золотого Ключика.", "Аполина Андреевна", 15m, "Биографическая книга о мыслях и жизни девушки по имени Марта", "image.jpg", null, "Марта", "Считаю до трёх, а потом как дам больно!" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Orders_OrderId",
                table: "CartItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Products_ProductId",
                table: "CartItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

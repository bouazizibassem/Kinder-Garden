using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class aa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "abonnement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(nullable: false),
                    PortfolioItemID = table.Column<int>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    NomJardin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abonnement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Date_of_Birth = table.Column<string>(nullable: true),
                    Date_Of_Recuit = table.Column<string>(nullable: true),
                    salaire = table.Column<int>(nullable: false),
                    NumTel = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEvent = table.Column<DateTime>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    DescriptionE = table.Column<string>(nullable: true),
                    ImageEvent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameImage = table.Column<string>(nullable: true),
                    DateInsert = table.Column<string>(nullable: true),
                    ImageG_Url = table.Column<string>(nullable: true),
                    ResponsableID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    ParentEmail = table.Column<string>(nullable: true),
                    MailGardin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "portfolioItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomJardin = table.Column<string>(nullable: true),
                    AddressJ = table.Column<string>(nullable: true),
                    NumTel = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    EmailGaredn = table.Column<string>(nullable: true),
                    ResponsableID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolioItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionIde = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Question_Reponse = table.Column<string>(nullable: true),
                    Parent_Name = table.Column<string>(nullable: true),
                    JardinID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: false),
                    lastName = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 150, nullable: false),
                    dateOfBirth = table.Column<string>(nullable: true),
                    password = table.Column<string>(maxLength: 50, nullable: false),
                    IsEmailVerified = table.Column<bool>(nullable: false),
                    ActivationCode = table.Column<Guid>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    UserTel = table.Column<string>(maxLength: 50, nullable: false),
                    username = table.Column<string>(maxLength: 50, nullable: false),
                    gender = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    admin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "ActivationCode", "admin", "Avatar", "ConfirmPassword", "dateOfBirth", "email", "firstName", "gender", "IsEmailVerified", "lastName", "password", "Role", "UserTel", "username" },
                values: new object[] { 1, new Guid("23db8396-f595-4a3c-9740-81af3ce25aa7"), true, "avatar.jpg", null, "Null", "user@gmail.com", "user", "male", false, "user", "user", null, "25252525", "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abonnement");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "portfolioItems");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

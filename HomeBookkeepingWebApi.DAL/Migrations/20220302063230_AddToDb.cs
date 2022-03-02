using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeBookkeepingWebApi.DAL.Migrations
{
    public partial class AddToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberCardUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOperations = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "СreditСard",
                columns: table => new
                {
                    СreditСardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R_Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_СreditСard", x => x.СreditСardId);
                    table.ForeignKey(
                        name: "FK_СreditСard_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "Category", "DateOperations", "NumberCardUser", "RecipientName", "Sum", "UserFullName" },
                values: new object[,]
                {
                    { 1, "Рестораны и кафе", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000 0000 0000 0000", "YABLONKA 9", 100m, "Горлов Андрей" },
                    { 2, "Рестораны и кафе", new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000 0000 0000 0000", "KFS", 50m, "Горлов Андрей" },
                    { 3, "Комунальные платежи, связь, интернет.", new DateTime(2022, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000 0000 0000 0001", "DOM.RU PENZA", 1000m, "Горлов Андрей" },
                    { 4, "Здоровье и красота", new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000 0000 0000 0002", "Летуаль", 70m, "Горлова Ольга" },
                    { 5, "Летуаль", new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000 0000 0000 0002", "Летуаль", 300m, "Горлова Ольга" },
                    { 6, "Одежда и аксессуары", new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "0000 0000 0000 0000", "OOO Dom Knigi", 150m, "Горлова Ольга" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "a@gmail.com", "Горлов Андрей", "013579" },
                    { 2, "o@gmail.com", "Горлова Ольга", "013789" }
                });

            migrationBuilder.InsertData(
                table: "СreditСard",
                columns: new[] { "СreditСardId", "CardName", "Number", "R_Account", "Sum", "UserFullName", "UserId" },
                values: new object[,]
                {
                    { 1, "Сбер", "0000 0000 0000 0000", "0000000000001", 8000m, "Горлов Андрей", 1 },
                    { 2, "ВТБ", "0000 0000 0000 0001", "0000000000002", 3000m, "Горлов Андрей", 1 },
                    { 3, "Сбер", "0000 0000 0000 0002", "0000000000003", 5000m, "Горлова Ольга", 2 },
                    { 4, "Мир", "0000 0000 0000 0000", "0000000000004", 3000m, "Горлова Ольга", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_СreditСard_UserId",
                table: "СreditСard",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "СreditСard");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

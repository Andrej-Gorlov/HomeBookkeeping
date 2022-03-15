using HomeBookkeepingWebApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Transaction>? Transaction { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<СreditСard>? СreditСard { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                FullName = "Горлов Андрей",
                Email = "a@gmail.com",
                PhoneNumber = "013579"
            }, new User
            {
                UserId = 2,
                FullName = "Горлова Ольга",
                Email = "o@gmail.com",
                PhoneNumber = "013789"
            });

            modelBuilder.Entity<СreditСard>().HasData(new СreditСard
            {
                СreditСardId = 1,
                UserId = 1,
                UserFullName = "Горлов Андрей",
                BankName = "Сбер",
                Number = "0000 0000 0000 0000",
                L_Account = "40817810100011234567",
                Sum = 8000
            }, new СreditСard
            {
                СreditСardId = 2,
                UserId = 1,
                UserFullName = "Горлов Андрей",
                BankName = "ВТБ",
                Number = "0000 0000 0000 0001",
                L_Account = "40817810200021234568",
                Sum = 3000
            }, new СreditСard
            {
                СreditСardId = 3,
                UserId = 2,
                UserFullName = "Горлова Ольга",
                BankName = "Сбер",
                Number = "0000 0000 0000 0002",
                L_Account = "40817810300031234569",
                Sum = 5000
            }, new СreditСard
            {
                СreditСardId = 4,
                UserId = 2,
                UserFullName = "Горлова Ольга",
                BankName = "Мир",
                Number = "0000 0000 0000 0000",
                L_Account = "40817810400041234560",
                Sum = 3000
            });

            modelBuilder.Entity<Transaction>().HasData(new Transaction
            {
                Id = 1,
                UserFullName = "Горлов Андрей",
                NumberCardUser = "0000 0000 0000 0000",
                RecipientName = "YABLONKA 9",
                DateOperations = new DateTime(2022, 1, 15),
                Sum = 100,
                Category = "Рестораны и кафе"
            }, new Transaction
            {
                Id = 2,
                UserFullName = "Горлов Андрей",
                NumberCardUser = "0000 0000 0000 0000",
                RecipientName = "KFS",
                DateOperations = new DateTime(2022, 1, 20),
                Sum = 50,
                Category = "Рестораны и кафе"
            }, new Transaction
            {
                Id = 3,
                UserFullName = "Горлов Андрей",
                NumberCardUser = "0000 0000 0000 0001",
                RecipientName = "DOM.RU PENZA",
                DateOperations = new DateTime(2022, 1, 22),
                Sum = 1000,
                Category = "Комунальные платежи, связь, интернет."
            }, new Transaction
            {
                Id = 4,
                UserFullName = "Горлова Ольга",
                NumberCardUser = "0000 0000 0000 0002",
                RecipientName = "Летуаль",
                DateOperations = new DateTime(2022, 1, 10),
                Sum = 70,
                Category = "Здоровье и красота"
            }, new Transaction
            {
                Id = 5,
                UserFullName = "Горлова Ольга",
                NumberCardUser = "0000 0000 0000 0002",
                RecipientName = "Летуаль",
                DateOperations = new DateTime(2022, 1, 30),
                Sum = 300,
                Category = "Летуаль"
            }, new Transaction
            {
                Id = 6,
                UserFullName = "Горлова Ольга",
                NumberCardUser = "0000 0000 0000 0000",
                RecipientName = "OOO Dom Knigi",
                DateOperations = new DateTime(2022, 2, 2),
                Sum = 150,
                Category = "Одежда и аксессуары"
            });
        }
    }
}

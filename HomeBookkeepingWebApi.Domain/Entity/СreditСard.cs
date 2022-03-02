using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity
{
    public class СreditСard
    {
        [Key]
        public int СreditСardId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Display(Name = "Название карты"), Required(ErrorMessage = "Укажите названия карты")]
        public string? CardName { get; set; }

        [Display(Name = "Название карты"), Required(ErrorMessage = "Укажите полное имя владельца кредитной карты")] 
        public string? UserFullName { get; set; }

        [Display(Name = "Номер карты"), Required(ErrorMessage = "Укажите номер карты")] 
        public string? Number { get; set; }

        [Display(Name = "Расчёт счёт"), Required(ErrorMessage = "Укажите р/c")] 
        public string? R_Account { get; set; }

        [Display(Name = "Баланс")] 
        public decimal Sum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Пользователь"), Required(ErrorMessage = "Укажите полное имя пользователя совершившего транзакцию.")]
        public string? UserFullName { get; set; }
        [Display(Name = "Кредитная карта"), Required(ErrorMessage = "Укажите номер карты пользователя совершившего транзакцию.")]
        public string? NumberCardUser { get; set; }

        [Display(Name = "Получатель"), Required(ErrorMessage = "Укажите ( имя/названия организации ) получателя")]
        public string? RecipientName { get; set; }

        [Display(Name = "Дата проведения операции")] 
        public DateTime DateOperations { get; set; }
        [Display(Name = "Сумма"), Required(ErrorMessage = "Укажите сумму операции")] 
        public decimal Sum { get; set; }
        [Display(Name = "Вид расхода"), Required(ErrorMessage = "Укажите категорию расхода")] 
        public string? Category { get; set; }
    }
}

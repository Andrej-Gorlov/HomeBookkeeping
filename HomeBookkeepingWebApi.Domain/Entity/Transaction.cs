using System.ComponentModel.DataAnnotations;

namespace HomeBookkeepingWebApi.Domain.Entity
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите полное имя пользователя совершившего транзакцию.")]
        public string? UserFullName { get; set; }
        
        [Required(ErrorMessage = "Укажите номер карты пользователя совершившего транзакцию.")]
        public string? NumberCardUser { get; set; }

        [Required(ErrorMessage = "Укажите ( имя/названия организации ) получателя")]
        public string? RecipientName { get; set; }

        [Required(ErrorMessage = "Укажите дату проведение операции")] 
        public DateTime DateOperations { get; set; }
        
        [Required(ErrorMessage = "Укажите сумму операции")] 
        public decimal Sum { get; set; }
       
        [Required(ErrorMessage = "Укажите категорию расхода")] 
        public string? Category { get; set; }
    }
}

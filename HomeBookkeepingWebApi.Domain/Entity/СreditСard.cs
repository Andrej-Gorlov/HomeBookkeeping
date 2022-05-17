using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBookkeepingWebApi.Domain.Entity
{
    public class СreditСard
    {
        [Key]
        public int СreditСardId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Укажите названия карты")]
        public string? BankName { get; set; }

        [Required(ErrorMessage = "Укажите полное имя владельца кредитной карты")] 
        public string? UserFullName { get; set; }

        [Required(ErrorMessage = "Укажите номер карты")] 
        public string? Number { get; set; }

        [Required(ErrorMessage = "Укажите р/c")] 
        public string? L_Account { get; set; }
        public decimal Sum { get; set; }
    }
}

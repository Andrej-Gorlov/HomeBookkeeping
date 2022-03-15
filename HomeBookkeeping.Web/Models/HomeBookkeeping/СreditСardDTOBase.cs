using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class СreditСardDTOBase
    {
        public int СreditСardId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Название банка")]
        public string? BankName { get; set; }
        [Display(Name = "Полное имя владельца")]
        public string? UserFullName { get; set; }
        [Display(Name = "Номер карты")]
        public string? Number { get; set; }
        [Display(Name = "Лицевой счёт")]
        public string? L_Account { get; set; }
        [Display(Name = "Баланс")]
        public decimal Sum { get; set; }
        public bool IsPageUser { get; set; }
    }
}

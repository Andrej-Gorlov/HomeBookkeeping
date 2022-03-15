using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class TransactionDTOBase
    {
        public int Id { get; set; }
        [Display(Name = "Пользователь")]
        public string? UserFullName { get; set; }
        [Display(Name = "Кредитная карта")]
        public string? NumberCardUser { get; set; }
        [Display(Name = "Получатель")]
        public string? RecipientName { get; set; }
        [Display(Name = "Дата проведения операции")]
        public DateTime DateOperations { get; set; }
        [Display(Name = "Сумма")]
        public decimal Sum { get; set; }
        [Display(Name = "Вид расхода")]
        public string? Category { get; set; }
    }
}

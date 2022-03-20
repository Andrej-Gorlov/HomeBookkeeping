using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class TransactionDTOBase
    {
        public int Id { get; set; }
        [Display(Name = "Пользователь"), Required(ErrorMessage = "Выберите пользователя")]
        public string? UserFullName { get; set; }
        [Display(Name = "Кредитная карта"), Required(ErrorMessage = "Выберите кредитную карту")]
        public string? NumberCardUser { get; set; }
        [Display(Name = "Получатель"), Required(ErrorMessage = "Укажите получателя")]
        public string? RecipientName { get; set; }
        [Display(Name = "Дата операции")]
        public DateTime DateOperations { get; set; }
        [Display(Name = "Сумма"), Required(ErrorMessage = "Укажите сумму")]
        public decimal Sum { get; set; }
        [Display(Name = "Вид категории"), Required(ErrorMessage = "Укажите категорию")]
        public string? Category { get; set; }
    }
}

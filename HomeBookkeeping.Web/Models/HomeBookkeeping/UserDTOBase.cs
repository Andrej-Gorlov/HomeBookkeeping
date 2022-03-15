using System.ComponentModel.DataAnnotations;

namespace HomeBookkeeping.Web.Models.HomeBookkeeping
{
    public class UserDTOBase
    {
        public int UserId { get; set; }
        [Display(Name = "Полное имя")]
        public string? FullName { get; set; }
        [Display(Name = "Кридитная карта")]
        public HashSet<СreditСardDTOBase>? СreditСards { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

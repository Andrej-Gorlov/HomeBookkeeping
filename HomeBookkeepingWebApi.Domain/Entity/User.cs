using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "Полное имя"), Required(ErrorMessage = "Укажите имя")] 
        public string? FullName { get; set; }

        [Display(Name = "Кридитная карта"), Required(ErrorMessage = "Укажите данные кридитной карты")]
        public HashSet<СreditСard>? СreditСard { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

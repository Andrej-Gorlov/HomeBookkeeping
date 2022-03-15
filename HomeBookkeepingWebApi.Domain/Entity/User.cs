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

        [Required(ErrorMessage = "Укажите имя")] 
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Укажите данные кридитной карты")]
        public HashSet<СreditСard>? СreditСards { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

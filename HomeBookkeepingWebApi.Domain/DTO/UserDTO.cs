
namespace HomeBookkeepingWebApi.Domain.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public HashSet<СreditСardDTO>? СreditСards { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

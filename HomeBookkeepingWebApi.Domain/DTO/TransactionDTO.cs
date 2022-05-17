
namespace HomeBookkeepingWebApi.Domain.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string? UserFullName { get; set; }
        public string? NumberCardUser { get; set; }
        public string? RecipientName { get; set; }
        public DateTime DateOperations { get; set; }
        public decimal Sum { get; set; }
        public string? Category { get; set; }
    }
}


namespace HomeBookkeepingWebApi.Domain.Entity.TemporaryData
{
    public class Report
    {
        public string? FullName { get; set; }
        public DateTime DateTime { get; set; }
        public List<TypeExpenseAndSum>? Сategories { get; set; }
        public decimal Sum { get; set; }
    }
    public class TypeExpenseAndSum
    {
        public string? NameTypeExpense { get; set; }
        public List<ReportRecipient>? Recipients { get; set; }
        public decimal SumTypeExpense { get; set; }
    }

}

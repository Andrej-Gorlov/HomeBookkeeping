namespace HomeBookkeeping.Web.Models
{
    public class ResponseBase
    {
        public object? Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string>? ErrorMessages { get; set; }
    }
}

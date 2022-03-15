namespace HomeBookkeeping.Web
{
    public static class StaticDitels
    {
        public static string? HomeBookkeepingApiBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}

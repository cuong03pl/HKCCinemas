namespace HKCCinemas.Helper
{
    public class QueryObject
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Keyword { get; set; } = null;

    }
}

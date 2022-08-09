namespace TourCompany.Application.Common.DataTransferObjects
{
    public class PaginatedList<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = null!;
        public int Count { get; set; }
    }
}

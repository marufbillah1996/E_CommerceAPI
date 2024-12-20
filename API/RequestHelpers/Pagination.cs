namespace API.RequestHelpers
{
    //When we create a new instance of pagination then this is what we're going to supply to the new class when we create it.
    public class Pagination<T>(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public int Count { get; set; } = count; // count of total product but this needs to happen after filtering and before pagination.
        public IReadOnlyList<T> Data { get; set; } = data;
    }
}

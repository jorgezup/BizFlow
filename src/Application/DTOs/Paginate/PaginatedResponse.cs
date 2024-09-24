namespace Application.DTOs.Paginate;

public class PaginatedResponse<T>
{
    public List<T> Data { get; set; } = new List<T>();
    public T? Result { get; set; }
    public int TotalRecords { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
}
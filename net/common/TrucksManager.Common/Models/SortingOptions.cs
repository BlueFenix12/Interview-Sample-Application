namespace TrucksManager.Common.Models;

public class SortingOptions<T> where T : struct, Enum
{
    public T? SortBy { get; set; }
    
    public SortDirection? SortDirection { get; set; }
}
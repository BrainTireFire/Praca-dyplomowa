using AutoMapper;

namespace pracadyplomowa;

public static class MapperExtensions
{
    public static PagedList<TDestination> MapPagedList<TSource, TDestination>(this IMapper mapper,
        PagedList<TSource> source)
    {
        var mappedItems = mapper.Map<List<TDestination>>(source);
        return new PagedList<TDestination>(mappedItems, source.TotalCount, source.CurrentPage, source.PageSize);
    }
}
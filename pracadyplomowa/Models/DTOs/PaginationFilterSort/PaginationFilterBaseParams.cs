﻿namespace pracadyplomowa;

public class PaginationFilterBaseParams
{
    private const int MaxPageSize = 69;
    public int PageNumber { get; set; } = 1; 
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public String OrderBy { get; set; } = "name";
}
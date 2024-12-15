using System.ComponentModel.DataAnnotations;

namespace Tournament.Core.Req;
public class RequestParams
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    [Range(2, 20)]
    public int PageSize { get; set; } = 5;
}

public class CompanyRequestParams : RequestParams
{
    public bool IncludeEmployees { get; set; } = false;
}

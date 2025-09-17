using System;

namespace ApiPerformanceComparison.FastEndpoints.Requests;

public class GetProductsListRequest
{
    public int? Count { get; set; } = 50;
}

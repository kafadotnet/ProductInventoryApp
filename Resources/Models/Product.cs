﻿
namespace Resources.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ProductName { get; set; } = null!;

    public string ProductSpecification { get; set; } = null!;

    public decimal ProductPrice { get; set; }
}

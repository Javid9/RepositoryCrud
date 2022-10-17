using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;

namespace RepositoryDemo.ViewModels;

public class ProductViewModel
{
    public Product Products { get; set; }
    public ProductUpdateDto ProductUpdateDto { get; set; }
}
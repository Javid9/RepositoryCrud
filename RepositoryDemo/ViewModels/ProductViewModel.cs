using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;

namespace RepositoryDemo.ViewModels;

public class ProductViewModel
{
    public Product Product { get; set; }
    public ProductUpdateDto ProductUpdateDto { get; set; }
}
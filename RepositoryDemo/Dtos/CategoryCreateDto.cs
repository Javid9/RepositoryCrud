using System.ComponentModel.DataAnnotations;

namespace RepositoryDemo.Dtos;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "Name is Required")]
    public string Name { get; set; }
}
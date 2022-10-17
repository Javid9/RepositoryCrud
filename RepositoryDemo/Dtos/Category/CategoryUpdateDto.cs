using System.ComponentModel.DataAnnotations;

namespace RepositoryDemo.Dtos;

public class CategoryUpdateDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name field is required")]
    public string Name { get; set; }
}
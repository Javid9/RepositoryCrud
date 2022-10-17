using System.ComponentModel.DataAnnotations;

namespace RepositoryDemo.ViewModels;

public class CreateOrderByUserDto
{
    public int Id { get; set; }

    [Required] public string FullName { get; set; }

    [Required] public string Email { get; set; }
}
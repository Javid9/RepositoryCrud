using System.ComponentModel.DataAnnotations;

namespace RepositoryDemo.Dtos;

public class LoginDto
{
    [Required,EmailAddress,DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required,DataType(DataType.Password)]
    public string Password { get; set; }

    public string RememberMe { get; set; }
}
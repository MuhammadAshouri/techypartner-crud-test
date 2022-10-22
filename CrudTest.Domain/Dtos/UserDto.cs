using System.ComponentModel.DataAnnotations;

namespace CrudTest.Domain.Dtos;

public class UserDto
{
    public int Id { get; set; }
    
    [MinLength(2)]
    [MaxLength(60)]
    public string FirstName { get; set; } = "";
    
    [MinLength(2)]
    [MaxLength(60)]
    public string LastName { get; set; } = "";
    
    [MinLength(3)]
    [MaxLength(200)]
    public string Email { get; set; } = "";
    
    [MinLength(4)]
    [MaxLength(10)]
    public string Gender { get; set; } = "";
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }
}

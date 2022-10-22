using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudTest.Domain.Models;

public class User : Entity
{
    [StringLength(60)]
    public string FirstName { get; set; } = "";
    
    [StringLength(60)]
    public string LastName { get; set; } = "";
    
    [StringLength(300)]
    public string Email { get; set; } = "";
    
    [StringLength(10)]
    public string Gender { get; set; } = "";
    
    [DefaultValue(false)]
    public bool Status { get; set; }
}

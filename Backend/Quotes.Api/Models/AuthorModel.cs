using System.ComponentModel.DataAnnotations;

namespace Quotes.Api.Models;

public class AuthorModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string LastName { get; set; } = string.Empty;
}

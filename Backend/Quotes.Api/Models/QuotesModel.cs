using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotes.Api.Models;

public class QuotesModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string Quote { get; set; } = string.Empty;

    [Required]
    public QuoteType Type { get; set; }

    [Required]
    public int AuthorId { get; set; } // Foreign Key

    [ForeignKey("AuthorId")]
    public AuthorModel AuthorNavProp { get; set; } // Navigation Property
}

namespace Quotes.Api.Models.DTOs;

public class QuotesDTO
{
    public int? Id { get; set; }

    public string? Quote { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ImageUrl { get; set; }
}

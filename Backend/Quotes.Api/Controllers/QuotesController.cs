using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotes.Api.Data;
using Quotes.Api.Models.DTOs;
using Quotes.Api.Models;
using Quotes.Api.Constants;

namespace Quotes.Api.Controllers;


[Route("api/quotes")]
[ApiController]
public class QuotesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public QuotesController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<QuotesDTO>>> GetAllQuotesAsync()
    {
        var quotes = await _db.QuotesDb.Select(row => new QuotesDTO {
            Id = row.Id,
            Quote = row.Quote,
            FirstName = row.AuthorNavProp.FirstName,
            LastName = row.AuthorNavProp.LastName,
            ImageUrl = row.AuthorNavProp.ImageUrl,
            Type = row.Type
        }).ToListAsync();

        if (quotes.Count == 0) {
            return NotFound(new ErrorResponse { Message = Messages.NoQuotesFound });
        }

        return Ok(quotes);
    }

    [HttpGet("{id:int}", Name = RouteName.GetQuoteById)]
    public async Task<ActionResult<QuotesDTO>> GetQuoteByIDAsync([FromRoute] int id) {

        if (id <= 0)
        {
            return BadRequest(new ErrorResponse { Message = Messages.InvalidId });
        }

        var quote = await _db.QuotesDb
            .Where(row => row.Id == id)
            .Select(row => new QuotesDTO
            {
                Id = row.Id,
                Quote = row.Quote,
                FirstName = row.AuthorNavProp.FirstName,
                LastName = row.AuthorNavProp.LastName,
                ImageUrl = row.AuthorNavProp.ImageUrl,
                Type = row.Type
            }
            ).FirstOrDefaultAsync();

        if (quote == null) {
            return NotFound(new ErrorResponse { Message = Messages.QuoteNotFound });
        }

        return Ok(quote);
    }

    [HttpPost]
    public async Task<IActionResult> AddQuoteAsync([FromBody] QuotesDTO response) {

        if (string.IsNullOrWhiteSpace(response.Quote) ||
            string.IsNullOrWhiteSpace(response.FirstName) ||
            string.IsNullOrWhiteSpace(response.LastName) ||
            (!response.Type.HasValue || !Enum.IsDefined(typeof(QuoteType), response.Type.Value))
            )
        {
            return BadRequest(new ErrorResponse { Message = Messages.MissingRequiredFields });
        }

        //check if author already exists
        var author = await _db.AuthorDb.FirstOrDefaultAsync(row => row.FirstName == response.FirstName && row.LastName == response.LastName);
        QuotesModel? quote = null;

        //update Author table 
        if (author == null) { // add new author

            var newAuthor = new AuthorModel { FirstName = response.FirstName, LastName = response.LastName, ImageUrl = response.ImageUrl ?? "" };

            await _db.AuthorDb.AddAsync(newAuthor);
            await _db.SaveChangesAsync();

            quote = new QuotesModel { Quote = response.Quote, Type = response.Type.Value, AuthorId = newAuthor.Id };
        }
        else {
            quote = new QuotesModel { Quote = response.Quote, Type = response.Type.Value, AuthorId = author.Id };
        }

        //update Quote table
        await _db.QuotesDb.AddAsync(quote);
        await _db.SaveChangesAsync();

        return CreatedAtRoute(RouteName.GetQuoteById, new { Id = quote.Id }, response);
    }

    [HttpPut("quote/{id:int}")]
    public async Task<IActionResult> UpdateQuoteAsync([FromRoute] int id, [FromBody] QuotesDTO response) {

        if (id <= 0)
        {
            return BadRequest(new ErrorResponse { Message = Messages.InvalidId });
        }

        var quote = await _db.QuotesDb.FindAsync(id);

        if (quote == null) return NotFound(new ErrorResponse { Message = Messages.QuoteNotFound });

        //Update Quote if its passed in DTO
        if (response.Quote != null)
        {
            quote.Quote = response.Quote;
        }

        if (response.Type.HasValue && Enum.IsDefined(typeof(QuoteType), response.Type.Value))
        {
            quote.Type = response.Type.Value; 
        }

        //Update Author if its passed in DTO
        if (response.FirstName != null && response.LastName != null)
        {
            //If already exists
            var author = await _db.AuthorDb
                .Select(row => new { Id = row.Id, FirstName = row.FirstName, LastName = row.LastName })
                .Where(row => row.FirstName == response.FirstName && row.LastName == response.LastName)
                .SingleOrDefaultAsync();

            if (author != null)
            {
                quote.AuthorId = author.Id;
            }
            else
            {
                //create new
                var newAuthor = new AuthorModel { FirstName = response.FirstName, LastName = response.LastName };

                await _db.AuthorDb.AddAsync(newAuthor);
                await _db.SaveChangesAsync();

                quote.AuthorId = newAuthor.Id;
            }

        }

        _db.QuotesDb.Update(quote);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("author/{id:int}")]
    public async Task<IActionResult> UpdateAuthorAsync([FromRoute] int id, [FromBody] QuotesDTO response)
    {

        if (id <= 0)
        {
            return BadRequest(new ErrorResponse { Message = Messages.InvalidId });
        }

        var author = await _db.AuthorDb.FindAsync(id);

        if (author == null) return NotFound(new ErrorResponse { Message = Messages.AuthorNotFound });

        if (response.FirstName != null)
        {
            author.FirstName = response.FirstName;
        }
        if (response.LastName != null)
        {
            author.LastName = response.LastName;
        }
        if (response.ImageUrl != null)
        {
            author.ImageUrl = response.ImageUrl;
        }

        _db.AuthorDb.Update(author);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("quote/{id:int}")]
    public async Task<IActionResult> DeleteQuoteAsync([FromRoute] int id)
    {

        if (id <= 0)
        {
            return BadRequest(new ErrorResponse { Message = Messages.InvalidId });
        }

        var quote = await _db.QuotesDb.FindAsync(id);

        if (quote == null) return NotFound(new ErrorResponse { Message = Messages.QuoteNotFound });

        _db.QuotesDb.Remove(quote);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("author/{id:int}")]
    public async Task<IActionResult> DeleteAuthorAsync([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(new ErrorResponse { Message = Messages.InvalidId });
        }

        var author = await _db.AuthorDb.FindAsync(id);

        if (author == null) return NotFound(new ErrorResponse { Message = Messages.AuthorNotFound });

        var quotes = await _db.QuotesDb.Where(row => row.AuthorId == author.Id).ToListAsync();

        if (quotes.Count > 0)
        {
            _db.QuotesDb.RemoveRange(quotes);
        }

        _db.AuthorDb.Remove(author);

        await _db.SaveChangesAsync();

        return NoContent();
    }
}

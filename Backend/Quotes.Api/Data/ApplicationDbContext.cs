namespace Quotes.Api.Data;

using Microsoft.EntityFrameworkCore;
using Quotes.Api.Models;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<QuotesModel> QuotesDb { get; set; }
    public DbSet<AuthorModel> AuthorDb { get; set; }

}

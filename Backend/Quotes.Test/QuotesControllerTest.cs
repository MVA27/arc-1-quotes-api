using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotes.Api.Constants;
using Quotes.Api.Controllers;
using Quotes.Api.Data;
using Quotes.Api.Models;
using Quotes.Api.Models.DTOs;

namespace Quotes.Test;

public class QuotesControllerTest
{
    private ApplicationDbContext? context = null;
    private QuotesController? quotesController = null;

    private async Task InitAsync(bool seedData = true)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new ApplicationDbContext(options);
        quotesController = new QuotesController(context);

        if (seedData)
        {
            var author = new AuthorModel
            {
                FirstName = "AuthorFirst",
                LastName = "AuthorLast",
                ImageUrl = "www.google.com/image"
            };

            await context.AuthorDb.AddAsync(author);
            await context.SaveChangesAsync();

            var quote = new QuotesModel
            {
                Quote = "Test quote",
                Type = QuoteType.Motivational,
                AuthorId = author.Id
            };

            await context.QuotesDb.AddAsync(quote);
            await context.SaveChangesAsync();
        }

    }

    [Fact]
    public async Task GetAllQuotes_ShouldReturn_NotFoundWhenQuotesDontExist()
    {
        //Arrange
        await InitAsync(false);

        //Act
        var actionResult = await quotesController.GetAllQuotes();

        //Assert
        var objectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.NoQuotesFound, errorResponse.Message);
    }
    
    [Fact]
    public async Task GetAllQuotes_ShouldReturn_OkWhenQuotesExist()
    {
        //Arrange
        await InitAsync();

        //Act
        var actionResult = await quotesController.GetAllQuotes();

        //Assert
        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var quotesList = Assert.IsAssignableFrom<List<QuotesDTO>>(objectResult.Value);
        Assert.Single(quotesList);
        Assert.Equal("Test quote", quotesList[0].Quote);
    }

    [Fact]
    public async Task GetQuoteByID_ShouldReturn_BadRequestWhenIdLessThanZero()
    {
        //Arrange
        await InitAsync(false);
        int id = 0;

        //Act
        var actionResult = await quotesController.GetQuoteByID(id);

        //Assert
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.InvalidId, errorResponse.Message);

    }

    [Fact]
    public async Task GetQuoteByID_ShouldReturn_OkWhenIdDontExists()
    {
        //Arrange
        await InitAsync();
        int id = 1;

        //Act
        var actionResult = await quotesController.GetQuoteByID(id);

        //Assert
        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var quotesDTO = Assert.IsType<QuotesDTO>(objectResult.Value);
        Assert.Equal("Test quote", quotesDTO.Quote);
        Assert.Equal(QuoteType.Motivational, quotesDTO.Type);
        Assert.Equal("AuthorFirst", quotesDTO.FirstName);
        Assert.Equal("AuthorLast", quotesDTO.LastName);
        Assert.Equal("www.google.com/image", quotesDTO.ImageUrl);

    }

    [Fact]
    public async Task GetQuoteByID_ShouldReturn_NotFoundWhenIdDontExists()
    {
        //Arrange
        await InitAsync(false);
        int id = 999;

        //Act
        var actionResult = await quotesController.GetQuoteByID(id);

        //Assert
        var objectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.QuoteNotFound, errorResponse.Message);
    }


    [Fact]
    public async Task AddQuote_ShouldAddAQuoteWhenInputIsValid()
    {
        //Arrange
        await InitAsync(false);
        QuotesDTO response = new QuotesDTO
        {
            Quote = "Test quote",
            Type = QuoteType.Motivational,
            FirstName = "AuthorFirst",
            LastName = "AuthorLast",
            ImageUrl = "www.google.com/image"
        };

        //Act
        var actionResult = await quotesController.AddQuote(response);

        //Assert
        var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult);
        Assert.Equal(response.Quote, ((QuotesDTO) createdAtRouteResult.Value).Quote);
        Assert.Equal("Test quote", ((QuotesDTO)createdAtRouteResult.Value).Quote);
    }

    [Fact]
    public async Task AddQuote_ShouldReturnBadRequestWhen_QuoteIsEmpty()
    {
        //Arrange
        await InitAsync(false);
        QuotesDTO response = new QuotesDTO
        {
            Quote = "",
            Type = QuoteType.Motivational,
            FirstName = "AuthorFirst",
            LastName = "AuthorLast",
            ImageUrl = "www.google.com/image"
        };

        //Act
        var actionResult = await quotesController.AddQuote(response);

        //Assert
        var objectRequest = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectRequest.Value);
        Assert.Equal(Messages.MissingRequiredFields, errorResponse.Message);
    }

    [Fact]
    public async Task AddQuote_ShouldReturnBadRequestWhen_FirstNameIsEmpty()
    {
        //Arrange
        await InitAsync(false);
        QuotesDTO response = new QuotesDTO
        {
            Quote = "Test quote",
            Type = QuoteType.Motivational,
            FirstName = "",
            LastName = "AuthorLast",
            ImageUrl = "www.google.com/image"
        };

        //Act
        var actionResult = await quotesController.AddQuote(response);

        //Assert
        var objectRequest = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectRequest.Value);
        Assert.Equal(Messages.MissingRequiredFields, errorResponse.Message);
    }

    [Fact]
    public async Task AddQuote_ShouldReturnBadRequestWhen_LastNameIsEmpty()
    {
        //Arrange
        await InitAsync(false);
        QuotesDTO response = new QuotesDTO
        {
            Quote = "Test quote",
            Type = QuoteType.Motivational,
            FirstName = "AuthorFirst",
            LastName = "",
            ImageUrl = "www.google.com/image"
        };

        //Act
        var actionResult = await quotesController.AddQuote(response);

        //Assert
        var objectRequest = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectRequest.Value);
        Assert.Equal(Messages.MissingRequiredFields, errorResponse.Message);
    }

    [Fact]
    public async Task AddQuote_ShouldReturnBadRequestWhen_TypeIsEmpty()
    {
        //Arrange
        await InitAsync(false);
        QuotesDTO response = new QuotesDTO
        {
            Quote = "Test quote",
            Type = null,
            FirstName = "AuthorFirst",
            LastName = "AuthorLast",
            ImageUrl = "www.google.com/image"
        };

        //Act
        var actionResult = await quotesController.AddQuote(response);

        //Assert
        var objectRequest = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectRequest.Value);
        Assert.Equal(Messages.MissingRequiredFields, errorResponse.Message);
    }

    [Fact]
    public async Task AddQuote_ShouldAddAQuoteWhenAuthorAlreadyExists()
    {
        //Arrange
        await InitAsync();
        QuotesDTO response = new QuotesDTO
        {
            Quote = "Test quote",
            Type = QuoteType.Motivational,
            FirstName = "AuthorFirst",
            LastName = "AuthorLast"
        };

        //Act
        var actionResult = await quotesController.AddQuote(response);

        //Assert
        var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult);
        Assert.Equal(response.Quote, ((QuotesDTO)createdAtRouteResult.Value).Quote);
        Assert.Equal(response.FirstName, ((QuotesDTO)createdAtRouteResult.Value).FirstName);
        Assert.Equal(response.LastName, ((QuotesDTO)createdAtRouteResult.Value).LastName);

    }


    [Fact]
    public async Task UpdateQuote_ShouldReturn_BadRequestWhenIdLessThanZero() 
    {
        //Arrange
        await InitAsync(false);

        QuotesDTO response = new QuotesDTO
        {
            Quote = "Test quote",
            Type = QuoteType.Motivational,
            FirstName = "AuthorFirst",
            LastName = "AuthorLast"
        };

        int id = 0;

        //Act
        var actionResult = await quotesController.UpdateQuote(id, response);

        //Assert
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.InvalidId, errorResponse.Message);

    }


    [Fact]
    public async Task UpdateQuote_ShouldReturn_NotFoundWhenIdDontExists()
    {
        //Arrange
        await InitAsync(false);

        QuotesDTO response = new QuotesDTO
        {
            Quote = "Test quote",
            Type = QuoteType.Motivational,
            FirstName = "AuthorFirst",
            LastName = "AuthorLast"
        };

        int id = 999;

        //Act
        var actionResult = await quotesController.UpdateQuote(id, response);

        //Assert
        var objectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.QuoteNotFound, errorResponse.Message);
    }

    [Fact]
    public async Task UpdateQuote_ShouldUpdateQuoteIfInputIsValid()
    {
        //Arrange
        await InitAsync();

        QuotesDTO response = new QuotesDTO
        {
            Id = 1,
            Quote = "Test quote update",
            Type = QuoteType.Love,
            FirstName = "AuthorFirst",
            LastName = "AuthorLast"
        };

        int id = 1;

        //Act
        var actionResult = await quotesController.UpdateQuote(id, response);
        var updatedQuote = await context.QuotesDb.FindAsync(id);

        //Assert
        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(response.Quote, updatedQuote.Quote);
        Assert.Equal(response.Type, updatedQuote.Type);

    }

    [Fact]
    public async Task UpdateAuthor_ShouldReturn_BadRequestWhenIdLessThanZero()
    {
        //Arrange
        await InitAsync(false);

        int id = 0;

        QuotesDTO response = new QuotesDTO
        {
            Id = id,
            FirstName = "Author update First",
            LastName = "Author update Last"
        };


        //Act
        var actionResult = await quotesController.UpdateAuthor(id, response);


        //Assert
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.InvalidId, errorResponse.Message);
    }

    [Fact]
    public async Task UpdateAuthor_ShouldReturn_NotFoundWhenIdDoesNotExist()
    {
        //Arrange
        await InitAsync(false);

        int id = 999;

        QuotesDTO response = new QuotesDTO
        {
            Id = id,
            FirstName = "Author update First",
            LastName = "Author update Last"
        };

        //Act
        var actionResult = await quotesController.UpdateAuthor(id, response);

        //Assert
        var objectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.AuthorNotFound, errorResponse.Message);
    }

    [Fact]
    public async Task UpdateAutho_ShouldUpdateAuthorIfInputIsValid()
    {
        //Arrange
        await InitAsync(true);

        int id = 1;

        QuotesDTO response = new QuotesDTO
        {
            Id = id,
            FirstName = "Author update First",
            LastName = "Author update Last"
        };


        //Act
        var actionResult = await quotesController.UpdateAuthor(id, response);


        //Assert
        var updatedAuthor = await context.AuthorDb.FindAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(response.FirstName, updatedAuthor.FirstName);
        Assert.Equal(response.LastName, updatedAuthor.LastName);

    }


    [Fact]
    public async Task DeleteQuote_ShouldReturn_BadRequestWhenIdLessThanZero() 
    {
        //Arrange
        await InitAsync(false);

        int id = 0;

        var actionResult = await quotesController.DeleteQuote(id);

        //Assert
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.InvalidId, errorResponse.Message);
    }

    [Fact]
    public async Task DeleteQuote_ShouldReturn_NotFoundWhenIdDoesNotExist() 
    {
        //Arrange
        await InitAsync(false);

        int id = 999;

        var actionResult = await quotesController.DeleteQuote(id);

        //Assert
        var objectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.QuoteNotFound, errorResponse.Message);
    }

    [Fact]
    public async Task DeleteQuote_ShouldDeleteQuoteIfIdIsValid() 
    {
        //Arrange
        await InitAsync(true);

        int id = 1;

        var actionResult = await quotesController.DeleteQuote(id);

        //Assert
        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        var deletedQuote = await context.QuotesDb.FindAsync(id);
        Assert.Null(deletedQuote);
    }


    [Fact]
    public async Task DeleteAuthor_ShouldReturn_BadRequestWhenIdLessThanZero()
    {
        //Arrange
        await InitAsync(false);

        int id = 0;

        var actionResult = await quotesController.DeleteAuthor(id);

        //Assert
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.InvalidId, errorResponse.Message);
    }

    [Fact]
    public async Task DeleteAuthore_ShouldReturn_NotFoundWhenIdDoesNotExist()
    {
        //Arrange
        await InitAsync(false);

        int id = 999;

        var actionResult = await quotesController.DeleteAuthor(id);

        //Assert
        var objectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
        var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(Messages.AuthorNotFound, errorResponse.Message);
    }

    [Fact]
    public async Task DeleteAuthor_ShouldDeleteQuoteIfIdIsValid()
    {
        //Arrange
        await InitAsync(true);

        int id = 1;

        var actionResult = await quotesController.DeleteAuthor(id);

        //Assert
        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        var deletedAuthor = await context.QuotesDb.FindAsync(id);
        Assert.Null(deletedAuthor);
    }

}
using LibraryBookStatus;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookDb>(option => option.UseInMemoryDatabase("BookList"));

var app = builder.Build();

app.MapGet("/bookStatuses", async (BookDb db) => await db.statuses.ToListAsync());

app.MapGet("/bookStatus/{id}", async (int id, BookDb db) => await db.statuses.FindAsync(id));

app.MapPost("/bookStatus", async (BookStatus book, BookDb db) =>
{
    db.statuses.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($"/bookStatus/{book.Id}", book);
});

app.MapPut("/bookStatus/{id}", async (int id, BookStatus updatedBook, BookDb db) =>
{
    var book = await db.statuses.FindAsync(id);
    if (book == null) return Results.NotFound();
    book.BookName = updatedBook.BookName;
    book.IsAvailable = updatedBook.IsAvailable;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("bookStatus/{id}", async (int id, BookDb db) =>
{
    var book = await db.statuses.FindAsync(id);
    if (book == null) return Results.NotFound();
    db.statuses.Remove(book);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.Run();

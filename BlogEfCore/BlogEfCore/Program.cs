using BlogEfCore.Context;
using BlogEfCore.Models;
using Microsoft.EntityFrameworkCore;

using var db = new BlogContext();

// Note: Este exemplo cria o banco antes de rodar o app.
Console.WriteLine($"Database local: {db.DbPath}.");

Console.WriteLine("\nInserindo um novo blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
await db.SaveChangesAsync();

Console.WriteLine("\nLendo um blog");
var blog = await db.Blogs
    .OrderBy(b => b.BlogId)
    .FirstAsync();

Console.WriteLine("\nID: " + blog.BlogId);
Console.WriteLine("URL: " + blog.Url);

Console.WriteLine("\nAtualizando o blog e adicionando um post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Titulo = "Hello World", Conteudo = "Criei um app usando EF Core!" });
await db.SaveChangesAsync();

Console.WriteLine("Blog Atualizado:");
Console.WriteLine("\nID: " + blog.BlogId);
Console.WriteLine("URL: " + blog.Url);

Console.WriteLine("Post:");
foreach (var post in blog.Posts)
{
    Console.WriteLine(post.PostId);
    Console.WriteLine(post.Titulo);
    Console.WriteLine(post.Conteudo);
}

Console.WriteLine("\nDeletando o blog");
db.Remove(blog);
await db.SaveChangesAsync();
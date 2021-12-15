using Microsoft.EntityFrameworkCore;
using var context = new BlogContext();
context.Database.EnsureCreated();

var blog = new Blog(
    Name: "My New Blog!",
    Description: "Desc of New Blog"
);

Post post1 = new(Title: "Hello World Post1.");
blog.Posts.Add(post1);

blog.Posts.Add(post1 with {Id = Guid.NewGuid(), Title = "Hello World Post2."});
blog.Posts.Add(new Post(Title: "Hello World Post3."));
blog.Posts.Add(new Post(Title: "Hello World Post4."));

context.Blogs?.Add(blog);
context.SaveChanges();

public record Blog (/*Guid Id, */string Name, string Description/*, List<Post> Posts*/)
{
    public Guid Id { get; init; } = Guid.NewGuid();
//    public string Name { get; set; }
//    public string Description { get; set; }
    public List<Post> Posts { get; init; } = new List<Post>();
}
public record Post(/*Guid Id, */string Title/*, DateTime Created*/)
{
    public Guid Id { get; init; } = Guid.NewGuid();
//    public string Title { get; set; }
    public DateTime Created { get; init; } = DateTime.Now;
}

public class BlogContext : DbContext
{
    public DbSet<Blog>? Blogs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
        optionsBuilder.UseCosmos("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", 
            "CosmosDbTest");
        base.OnConfiguring(optionsBuilder);
    }
}
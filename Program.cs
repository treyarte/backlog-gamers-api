using backlog_gamers_api.Repositories;
using backlog_gamers_api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DP Injection
builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>(provider => new ArticlesRepository("articles"));
builder.Services.AddScoped<IArticleTagsRepo, ArticleTagRepo>(provider => new ArticleTagRepo("articleTags"));
builder.Services.AddScoped<IArticleSourceRepo, ArticleSourceRepo>(provider => new ArticleSourceRepo("articleSources"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

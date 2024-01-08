using Bookfy.Authors.Api.Adapters;
using Bookfy.Authors.Api.Boundaries;
using Bookfy.Authors.Api.Ports;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration
        .GetSection(nameof(MongoDbSettings))
);

builder.Services.AddSingleton<IMongoClient>(new MongoClient(
    Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING", EnvironmentVariableTarget.Process)
));

builder.Services.AddScoped<IAuthorUseCase, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorMongoDb>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/v1")
   .MapAuthorRoutes();

app.Run();

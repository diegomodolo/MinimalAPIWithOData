using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using MinimalAPIWithOData.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("CustomerOrderLists"));
builder.Services.AddOData(opt => opt.EnableAll());

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Seed();

var model = EdmModelBuilder.GetEdmModel();

var odataApp = app.MapGroup("odata")
    .WithODataResult()
    .WithODataModel(model)
    .WithODataBaseAddressFactory(h => new Uri("https://localhost:7019/odata"));

odataApp.MapGet("customers", (AppDbContext db, ODataQueryOptions<Customer> queryOptions) =>
    queryOptions.ApplyTo(db.Customers));

app.MapODataMetadata("odata/$metadata", model);

app.UseHttpsRedirection();

app.Run();
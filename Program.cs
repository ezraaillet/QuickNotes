using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using NotesApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

// add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
        });
}); 


builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddControllers();

builder.Services.AddDbContext<Context>(
        options => options.UseSqlServer("Server=EZRAAILLETSTREA;Database=myApp;Trusted_Connection=True;"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.MapControllers();


app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthentication();
// app.UseAuthorization();

app.Run();

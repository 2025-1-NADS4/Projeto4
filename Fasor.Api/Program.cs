
using Fasor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 


builder.Services.AddControllers();
 

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


app.UseAuthorization();
app.MapControllers();
app.Run();


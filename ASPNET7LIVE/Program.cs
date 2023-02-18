using ASPNET7LIVE.Services.ThaiDate;
using ASPNET7LIVE.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//>>Add database โดย APIContect คือตัวแทน DB
builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext")));
//--//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom Service
//การทำ di
//AddScoped -> ทุกๆ req จะ new create intance พอเสร็จก็ตายไป
//ทำให้เรียกใช้ Class Thaidate ได้  
builder.Services.AddScoped<IThaiDate, ThaiDate>();
//--//


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

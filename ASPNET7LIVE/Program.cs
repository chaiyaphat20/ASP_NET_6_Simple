using ASPNET7LIVE.Services.ThaiDate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom Service
//การทำ di
//AddScoped -> ทุกๆ req จะ new create intance พอเสร็จก็ตายไป
//ทำให้เรียกใช้ Class Thaidate ได้  
builder.Services.AddScoped<IThaiDate, ThaiDate>();

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

using ASPNET7LIVE.Services.ThaiDate;
using ASPNET7LIVE.Data;
using Microsoft.EntityFrameworkCore;
using ASPNET7LIVE.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


//>>Add database โดย APIContect คือตัวแทน DB
builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext")));
//--//


//>> Identity db
//options.SignIn.RequireConfirmedAccount = true  เมื่อ true ต้องมีการ comfirm mail หรือ admin confirm
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

//set context
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext")));

//config identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3; //มี password 3 ตัวได้
    options.Password.RequireNonAlphanumeric = false; //ให้กรอก 123456 ได้
    options.Password.RequireDigit = false; //มีตัวเลขผสม =false
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});
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

app.UseStaticFiles();  //สามารถ อัพ load file ได้

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//global cors policy
app.UseCors(options => options
        //.WithOrigins("https://example.com", "https://codingthailand.com")
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);


app.UseHttpsRedirection();

app.UseAuthentication();;
app.UseAuthorization();

app.MapControllers();

app.Run();

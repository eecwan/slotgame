using Microsoft.EntityFrameworkCore;
using slotgame.Data;

var builder = WebApplication.CreateBuilder(args);

// 加入 Controller
builder.Services.AddControllers();

// 設定資料庫
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 合併 AllowFrontend + AllowMVC 成一個 Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllClients", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",    // 前端開發頁面 (Vue / React)
            "http://localhost:5243"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// 啟用 CORS
app.UseCors("AllowAllClients");

// 啟用 HTTPS 導向
app.UseHttpsRedirection();

// 註冊 Controller
app.MapControllers();

// 開發模式可啟用 OpenAPI（可選）
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

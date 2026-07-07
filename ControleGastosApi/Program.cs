using Microsoft.EntityFrameworkCore;

using ControleGastosApi.Data;
 
var builder = WebApplication.CreateBuilder(args);
 
//  - Configuração do Banco de Dados SQLite local

builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseSqlite("Data Source=ControleGastos.db"));
 
//  Configuração de CORS para permitir requisições do front-end React (Vite usa a porta 5173)

builder.Services.AddCors(options =>

{

    options.AddPolicy("AllowReactApp", policy =>

    {

        policy.WithOrigins("http://localhost:5173")

              .AllowAnyHeader()

              .AllowAnyMethod();

    });

});
 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
 
var app = builder.Build();
 
if (app.Environment.IsDevelopment())

{

    app.UseSwagger();

    app.UseSwaggerUI();

}
 
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();
 
app.Run();
 
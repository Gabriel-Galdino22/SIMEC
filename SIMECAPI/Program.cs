using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using SIMECAPI.Data;
using SIMECAPI.Repositories;
using SIMECAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados com logs para depuração
builder.Services.AddDbContext<SIMECContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"))
           .EnableSensitiveDataLogging() // Habilita logs de dados sensíveis
           .LogTo(Console.WriteLine, LogLevel.Information)); // Loga as consultas no console

// Registrar os repositórios no contêiner de dependências
builder.Services.AddScoped<IApartamentoRepository, ApartamentoRepository>();
builder.Services.AddScoped<IConsumoEnergiaRepository, ConsumoEnergiaRepository>();
builder.Services.AddScoped<IDicaSustentabilidadeRepository, DicaSustentabilidadeRepository>();
builder.Services.AddScoped<IMoedaRepository, MoedaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Registrar o serviço de IA no contêiner de dependências
// Adicionar o serviço de IA
builder.Services.AddScoped<IIARecomendacaoService, IARecomendacaoService>();

// Adicionar suporte a CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Adicionar serviços de controle e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativar o middleware de CORS antes de outros middlewares
app.UseCors("AllowAll");

// Configuração do Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIMECAPI v1");
        c.RoutePrefix = "swagger"; // Define o prefixo como 'swagger'
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

using SBMS.API.Configurations;
using SBMS.API.Validators;

// Vars
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:4200") // tu Angular
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Para validar los tipos de datos del modelo recibido 
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomValidationFilter>();
});

// Para activar las validaciones personalizadas
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Add Registers: services, repositories, dataBase, Validators
builder.Services.LoggerConfigurations();
builder.Services.RegistrerDataBase();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterRepositories();
builder.Services.RegisterApplicationValidators();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

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
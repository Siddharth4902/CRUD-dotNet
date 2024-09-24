
using CRUD_PG.DAO;
using Npgsql;

namespace CRUD_PG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connection = builder.Configuration.GetConnectionString("Postgres");
            builder.Services.AddScoped((provider) => new NpgsqlConnection(connection));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                options.AddPolicy("FrontEndClient", builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000/"));
            });

            builder.Services.AddScoped<IEmployeeDAO, EmployeeDAOImplementation>();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAny");
            app.UseCors("FrontEndClient");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

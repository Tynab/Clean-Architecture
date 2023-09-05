using Application.Behaviors;
using Domain.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data;
using Web.Middlerware;
using static System.AppContext;
using static System.IO.Path;

namespace Web;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

        _ = services.AddControllers().AddApplicationPart(presentationAssembly);

        var applicationAssembly = typeof(Application.AssemblyReference).Assembly;

        _ = services.AddMediatR(applicationAssembly);
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        _ = services.AddValidatorsFromAssembly(applicationAssembly);

        _ = services.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments(Combine(BaseDirectory, $"{presentationAssembly.GetName().Name}.xml"));

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Web",
                Version = "v1"
            });
        });

        _ = services.AddDbContext<ApplicationDbContext>(b => b.UseNpgsql(Configuration.GetConnectionString("Default")));
        _ = services.AddScoped<IWebinarRepository, WebinarRepository>();
        _ = services.AddScoped<IUnitOfWork>(f => f.GetRequiredService<ApplicationDbContext>());
        _ = services.AddScoped<IDbConnection>(f => f.GetRequiredService<ApplicationDbContext>().Database.GetDbConnection());
        _ = services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            _ = app.UseDeveloperExceptionPage();
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
        }

        _ = app.UseMiddleware<ExceptionHandlingMiddleware>();
        _ = app.UseHttpsRedirection();
        _ = app.UseRouting();
        _ = app.UseAuthorization();
        _ = app.UseEndpoints(e => e.MapControllers());
    }
}

using Expenses.Application.Behaviours;
using Expenses.Application.Features.Accounts.Queries;
using Expenses.Application.Filters;
using Expenses.Domain.Mappings;
using Expenses.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Expenses.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // DbContext
            services.AddDbContext<ExpensesDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            var assembly = Assembly.GetExecutingAssembly();

            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAccountQueryHandler>());

            //fluentValidation
            services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<GetAccountQuery>());

            //Automapper
            services.AddAutoMapper(typeof(RoleProfile).Assembly);

            
            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

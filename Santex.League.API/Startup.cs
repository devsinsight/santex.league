using AutoMapper;
using Hangfire;
using Hangfire.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Santex.League.API.Configuration;
using Santex.League.API.Services;
using Santex.League.Domain.Repository;
using Santex.League.Proxy.Handler;
using Santex.League.Proxy.Utils;
using Santex.League.Queue;
using Santex.League.Repository;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Santex.League.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("LeagueDbConnection");

            services.AddAutoMapper();
            services
                .AddMvc();

            services.AddHttpClient();
            services.AddMediatR(typeof(CompetitionHandler));

            services
                 .AddDbContext<LeagueDbContext>(options =>
                        options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IFootballCredentials, FootballCredentials>();

            services.AddTransient<ILeagueService, LeagueService>();
            services.AddTransient<ICompetitionRepository, CompetitionRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();

            services.AddTransient<ITeamQueue, TeamQueue>();

            services.AddHangfire(options => {
                options.UseSqlServerStorage(connectionString);
                JobHelper.SetSerializerSettings(new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            });

            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                                   new Info
                                   {
                                       Title = SwaggerConfiguration.DocInfoTitle,
                                       Version = SwaggerConfiguration.DocInfoVersion,
                                       Description = SwaggerConfiguration.DocInfoDescription,
                                       Contact = contact
                                   });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<LeagueDbContext>();
                context.Database.EnsureCreated();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var options = new BackgroundJobServerOptions
            {
                ServerTimeout = TimeSpan.FromSeconds(100),
                WorkerCount = 1
            };

            app.UseHangfireServer(options);
            app.UseHangfireDashboard();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

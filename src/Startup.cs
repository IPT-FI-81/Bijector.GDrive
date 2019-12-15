using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Bijector.GDrive.Configs;
using Bijector.GDrive.Services;
using Bijector.GDrive.Models;
using Bijector.Infrastructure.Dispatchers;
using Bijector.Infrastructure.Discovery;
using Bijector.Infrastructure.Repositories;
using Bijector.Infrastructure.Queues;
using Bijector.Infrastructure.Handlers;
using Bijector.GDrive.Messages.Commands;
using Bijector.GDrive.Messages.Queries;
using Bijector.GDrive.Handlers.Commands;
using Bijector.GDrive.Handlers.Queries;
using System.Collections.Generic;
using Google.Apis.Drive.v3.Data;

namespace GDrive
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConsul(Configuration);

            services.AddRabbitMQ(Configuration);

            services.AddMongoDb(Configuration);
            services.AddMongoDbRepository<Token>("GDrive tokens");

            services.AddScoped<IServiceIdValidatorService, ServiceIdValidatorService>();
            services.AddTransient<IGoogleAuthService, GoogleAuthService>();

            services.AddHandleDispatchers();
            
            services.AddTransient<ICommandHandler<MoveDriveEntity>, MoveDriveEntityHandler>();
            services.AddTransient<ICommandHandler<RenameDriveEntity>, RenameDriveEntityHandler>();
            services.AddTransient<IQueryHandler<GetDirectories, IEnumerable<File>>, GetDirectoriesHandler>();
            services.AddTransient<IQueryHandler<GetFiles, IEnumerable<File>>, GetFilesHandler>();
            services.AddTransient<IQueryHandler<GetDriveEntity, File>, GetDriveEntityHandler>();

            services.Configure<GoogleConfigs>(Configuration.GetSection("GoogleOptions"));            

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;              
                options.Authority = "http://localhost:5000";
                options.Audience = "api.v1";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            
            services.AddAuthorization();            

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();            

            app.UseConsul(lifetime);

            app.UseRouting();            

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRabbitMQ().
                SubscribeCommand<MoveDriveEntity>().
                SubscribeCommand<RenameDriveEntity>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });
        }
    }
}

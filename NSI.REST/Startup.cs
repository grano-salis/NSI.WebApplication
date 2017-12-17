using System;
using Microsoft.AspNetCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;
using NSI.REST.Middleware;
using NSI.Repository.Interfaces;
using NSI.Repository;
using NSI.BLL.Interfaces;
using NSI.BLL;
using IkarusEntities;
using Swashbuckle.AspNetCore.Swagger;
using NSI.Repository.Repository;
using AutoMapper;

using IkarusEntities;
using NSI.BLL.Interfaces;
using NSI.BLL;
using NSI.Repository.Mappers;
using AddressRepository = NSI.Repository.AddressRepository;
using AddressTypeRepository = NSI.Repository.AddressTypeRepository;
using CaseInfoRepository = NSI.Repository.CaseInfoRepository;
using HearingsRepository = NSI.Repository.Repository.HearingsRepository;
using MeetingsRepository = NSI.Repository.MeetingsRepository;
using TaskRepository = NSI.Repository.TaskRepository;

namespace NSI.REST
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDataProtection();
            services.AddLocalization(options => options.ResourcesPath = "Resouces");
            services.AddMemoryCache();

            // Dependancy Injection
            services.AddSingleton<IConfiguration>(sp => { return Configuration; });
            services.AddDbContext<IkarusContext>();

            services.AddScoped<IAddressManipulation, AddressManipulation>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressTypeManipulation, AddressTypeManipulation>();
            services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
            services.AddScoped<IDocumentManipulation, DocumentManipulation>();
            services.AddScoped<IDocumentRepository, DocumentsRepository>();
            services.AddScoped<IMeetingsRepository, MeetingsRepository>();
            services.AddScoped<IMeetingsManipulation, MeetingsManipulation>();
            services.AddScoped<ITaskManipulation, TaskManipulation>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<IContactsManipulation, ContactsManipulation>();
      			services.AddScoped<ICaseInfoManipulation, CaseInfoManipulation>();
      			services.AddScoped<ICaseInfoRepository, CaseInfoRepository>();
            services.AddScoped<IConversationsRepository, ConversationsRepository>();
            services.AddScoped<IConversationsManipulation, ConversationsManipulation>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersManipulation, UsersManipulation>();
            services.AddScoped<IHearingsRepository, HearingsRepository>();
            services.AddScoped<IHearingsManipulation, HearingsManipulation>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionManipulation, TransactionManipulation>();
            services.AddScoped<IPaymentGatewayRepository, PaymentGatewayRepository>();
            services.AddScoped<IPaymentGatewayManipulation, PaymentGatewayManipulation>();
            services.AddScoped<IPricingPackageRepository, PricingPackageRepository>();
            services.AddScoped<IPricingPackageManipulation, PricingPackageManipulation>();
            //  services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerManipulation, CustomerManipulation>();
            // services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientManipulation, ClientManipulation>();

            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddDbContext<dbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("EntityCS")), ServiceLifetime.Transient);

            //Add AutoMapper
            services.AddAutoMapper();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Title = "NSI API",
                    Version = "v1",
                    Description = "Postojece metode u NSI.Rest aplikaciji",
                TermsOfService = "None"
                });
            });

            services.Configure<MvcOptions>(options => {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllHeaders"));
            });





            //services.AddTransient<BLL.Interfaces.IUserManagement, BLL.Users.UserManagement>();

            // Inject JWT Settings
            var jwtAppSettings = Configuration.GetSection("JwtIssuerOptions");
            services.Configure<JwtIssuerOptions>(opt =>
            {
                opt.Audience = jwtAppSettings["Audience"];
                opt.Issuer = jwtAppSettings["Issuer"];
                opt.TokenName = jwtAppSettings["TokenName"];
                opt.ValidFor = TimeSpan.Parse(jwtAppSettings["ValidFor"]);
                opt.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettings["SecretKey"])), SecurityAlgorithms.HmacSha256); // TODO: Get it from save location e.g. environment settings
            });

            // ********************
            // Setup CORS
            // ********************
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAllHeaders", corsBuilder.Build());
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowAllHeaders");

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US"),
                SupportedCultures = new[]
                {
                    new System.Globalization.CultureInfo("en-US"),
                    new System.Globalization.CultureInfo("en")
                },
                SupportedUICultures = new[]
                {
                    new System.Globalization.CultureInfo("en-US"),
                    new System.Globalization.CultureInfo("en")
                }
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSI API V1");
            });

            app.UseExHandler();
            //enableti ovaj dio na produkciji
            //dodati neki flag
            //app.UseAuthHandler();

            app.UseMvc();
        }
    }
}

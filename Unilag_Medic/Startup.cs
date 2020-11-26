using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.MemoryStorage;
using Unilag_Medic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Unilag_Medic.Helpers;

namespace Unilag_Medic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // public Startup(IConfiguration configuration) 
        // {
        //     this.Configuration = configuration;

        // }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the Dependency Injection container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure strongly typed settings objects
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Configure DI for application service
            services.AddScoped<IUserService, UserService>();

            //services.AddSingleton<IConfiguration>();
            services.AddSingleton<IZenossOps, ZenossOps>();
            services.AddHttpClient();

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage()
            );

            //services.AddHangfireServer();

            services.AddSingleton<ICronServices, CronServices>();

            services.AddCors();

            services.AddControllers();

            //services.AddDbContext<ApplicationDbContext>(
            //    option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //services.AddIdentity<IdentityUser, IdentityRole>(
            //    option =>
            //    {
            //        option.Password.RequireDigit = false;
            //        option.Password.RequiredLength = 10;
            //        option.Password.RequireLowercase = false;
            //        option.Password.RequireUppercase = false;
            //        option.Password.RequireNonAlphanumeric = false;
            //    }).AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddAuthentication(
                option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = Configuration["Jwt:Site"],
                        ValidAudience = Configuration["Jwt:Site"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"]))
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
        IWebHostEnvironment env,
        IBackgroundJobClient backgroundJobClient,
        IRecurringJobManager recurringJobManger,
        IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvc();

            //app.UseHangfireDashboard();
            //backgroundJobClient.Enqueue(() => Console.WriteLine("Hello hangfire!"));
            // recurringJobManger.AddOrUpdate(
            //     "Run Daily",
            //() => serviceProvider.GetService<ICronServices>().UpdateDependent(),
            //     Cron.Daily
            // );
            //backgroundJobClient.Schedule(() => serviceProvider.GetService<ICreateClinic>().InsertClinic(), TimeSpan.FromMinutes(2));


        }
    }
}

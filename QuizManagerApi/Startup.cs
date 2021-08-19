using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using QuizManagerApi.Domain.Connections;
using QuizManagerApi.Domain.IService;
using QuizManagerApi.Domain.Services;

namespace QuizManagerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add cross origin
            services.AddCors();

            MySqlConnection conn = GetConnection();

            services.AddTransient(_ => conn);

            services.AddControllers();

            //services.AddSingleton<IQuizService, QuizService>();
            services.Add(new ServiceDescriptor(typeof(UsersConnection), new UsersConnection(conn)));
            services.Add(new ServiceDescriptor(typeof(UserAccessConnection), new UserAccessConnection(Configuration.GetConnectionString(""))));
            services.Add(new ServiceDescriptor(typeof(AccessLevelConnection), new AccessLevelConnection(Configuration.GetConnectionString(""))));
            services.Add(new ServiceDescriptor(typeof(QuestionConnection), new QuestionConnection(conn)));
            services.Add(new ServiceDescriptor(typeof(AnswerOptionConnection), new AnswerOptionConnection(conn)));
            services.Add(new ServiceDescriptor(typeof(QuizConnection), new QuizConnection(conn)));

            //services.AddSingleton(new UsersConnection(conn));
            //services.AddSingleton(new UserAccessConnection(Configuration.GetConnectionString("")));
            //services.AddSingleton<IUserService, UserService>();
            //services.AddSingleton(new AccessLevelConnection(Configuration.GetConnectionString("")));
            //services.AddSingleton(new QuestionConnection(Configuration.GetConnectionString("")));
            //services.AddSingleton(new AnswerOptionConnection(Configuration.GetConnectionString("")));
        }

        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection("Server=localhost; port=3306; Database=QuizManager; Uid=root; Pwd=password");
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use cross origin
            app.UseCors(bldr => bldr.WithOrigins("http://localhost:8080")
                .WithMethods("GET", "POST")
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

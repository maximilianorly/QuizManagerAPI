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
using QuizManagerApi.Domain.Connections;

namespace QuizManagerApi
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
            services.AddControllers();
            services.Add(new ServiceDescriptor(typeof(UsersConnection), new UsersConnection(Configuration.GetConnectionString(""))));
            services.Add(new ServiceDescriptor(typeof(UserAccessConnection), new UserAccessConnection(Configuration.GetConnectionString(""))));
            services.Add(new ServiceDescriptor(typeof(AccessLevelConnection), new AccessLevelConnection(Configuration.GetConnectionString(""))));
            services.Add(new ServiceDescriptor(typeof(QuestionConnection), new QuestionConnection(Configuration.GetConnectionString(""))));
            services.Add(new ServiceDescriptor(typeof(AnswerOptionConnection), new AnswerOptionConnection(Configuration.GetConnectionString(""))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

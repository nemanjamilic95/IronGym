using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Application.Commands;
using Application.Commands.UserCommands;
using Application.Helpers;
using Application.Interfaces;
using EfCommands;
using EfDataAccess;
using IronGym.API.Email;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IronGym.API
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
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<IronContext>();
            services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
            services.AddTransient<IGetUserCommand,EfGetUserComand>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
            services.AddTransient<IGetRoleCommand, EfGetRoleCommand>();
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();
            services.AddTransient<IGetCommentsCommand, EfGetCommentsCommand>();
            services.AddTransient<IGetCommentCommand, EfGetCommentCommand>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IEditCommentCommand, EfEditCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();
            services.AddTransient<IGetLikesCommand, EfGetLikesCommand>();
            services.AddTransient<ICreateLikeCommand, EfCreateLikeCommand>();
            services.AddTransient<IDeleteLikeCommand, EfDeleteLikeCommand>();
            services.AddTransient<ICreateProgramCommand, EfCreateProgramCommand>();
            services.AddTransient<IGetProgramsCommand, EfGetProgramsCommand>();
            services.AddTransient<IGetProgramCommand, EfGetProgramCommand>();
            services.AddTransient<IEditProgramCommand, EfEditProgramCommand>();
            services.AddTransient<IDeleteProgramCommand, EfDeleteProgramCommand>();
            services.AddTransient<IGetPostsCommand, EfGetPostsCommand>();
            services.AddTransient<IGetPostCommand, EfGetPostCommand>();
            services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
            services.AddTransient<IEditPostCommand, EfEditPostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthorizeCommand, LoginCommand>();

            var email = Configuration.GetSection("Email");
            var sender = new SmtpEmailSender(email["host"], Int32.Parse(email["port"]), email["fromAddress"], email["password"]);
            services.AddSingleton<IEmailSender>(sender);


            var key = Configuration.GetSection("Encryption")["key"];
            var encryption = new Encryption(key);
            services.AddSingleton(encryption);

            services.AddTransient(s => {
            var http = s.GetRequiredService<IHttpContextAccessor>();
            var value = http.HttpContext.Request.Headers["Authorization"].ToString();
            var enc = s.GetRequiredService<Encryption>();
                try
                {
                    var decoded = enc.DecryptString(value);
                    decoded = decoded.Substring(0, decoded.LastIndexOf("}") + 1);
                    var user = JsonConvert.DeserializeObject<LoggedUser>(decoded);
                    user.IsLogged = true;
                    return user;
                }
                catch (Exception)
                {
                    return new LoggedUser
                    {
                        IsLogged=false
                    };
                }
                });

                

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();
           
        }
    }
}

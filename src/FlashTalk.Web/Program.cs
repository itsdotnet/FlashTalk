using FlashTalk.Web.Hubs;
using FlashTalk.Web.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FlashTalk.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapHub<ChatHub>("chat-hub");
            app.MapPost("broadcast", async (string message, IHubContext<ChatHub, IChatClient> context) =>
            {
                await context.Clients.All.ReceiveMessage(message);
                return Results.NoContent();
            });
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
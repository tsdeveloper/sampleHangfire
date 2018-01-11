using System;
using Hangfire;
using Hangfire.SqlServer;
using HangFire.Mailer.Attribute;
using Microsoft.Owin;
using Owin;

namespace HangFire.Mailer
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(
                    "MailerDb",
                    new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(10) })
                .UseFilter(new LogFailureAttribute());
            //var storage = new SqlServerStorage("MailerDb", new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(10) });
            


            //app.UseHangfireDashboard("/Scheduler", new DashboardOptions() { AuthorizationFilters = new[] { new HangFireAuthorizationFilter() } }, storage);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
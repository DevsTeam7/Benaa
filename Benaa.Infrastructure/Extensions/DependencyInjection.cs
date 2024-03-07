using Benaa.Infrastructure.Hubs;
using Benaa.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Benaa.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                //Q:How to add job for every chat and make it start in a specfiec time and day
                var jobkey = JobKey.Create(nameof(ScheduleBackgroundJob));
                options
                .AddJob<ScheduleBackgroundJob>(jobkey)
                .AddTrigger(trigger =>
                trigger.
                ForJob(jobkey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(1).RepeatForever())
                

                ); ;

            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });


        }

        public static void AddHubs(this IEndpointRouteBuilder app)
        {
            app.MapHub<ChatHub>("/chathub");
        }

    }
}

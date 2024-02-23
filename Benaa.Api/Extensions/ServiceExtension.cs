using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Benaa.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Benaa.Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<IScedualeService, ScedualeService>();
            #endregion

            #region Repositories
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IMoneyCodeRepository, MoneyCodeRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IPaymentRepositoty, PaymentRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<ISchedualRepository, SchedualRepository>();
            #endregion


            return services;
        }
    }
}

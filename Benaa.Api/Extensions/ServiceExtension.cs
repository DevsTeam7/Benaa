using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.Authentication;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Benaa.Infrastructure.Repositories;
using Benaa.Infrastructure.Services;
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
            services.AddScoped<IMoneyCodeService, MoneyCodeService>();
            services.AddScoped<IScedualeService, ScedualeService>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<IScedualeService, ScedualeService>();
            services.AddScoped<IChatHubService, ChatHubService>();
            services.AddScoped<ITokenGeneration, TokenGeneration>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<ICourseService, CourseService>();
            #endregion

            #region Repositories
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IMoneyCodeRepository, MoneyCodeRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IPaymentRepositoty, PaymentRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<ISchedualRepository, SchedualRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IOTPCodesRepository, OTPCodesRepository>();
            services.AddTransient<IChapterRepository, ChapterRepository>();
            services.AddTransient<ILessonRepository, LessonRepository>();
            services.AddTransient<IUserCoursesRepository, UserCoursesRepository>();
            services.AddTransient<IRateRepository, RateRepository>();
            #endregion


            return services;
        }
    }
}

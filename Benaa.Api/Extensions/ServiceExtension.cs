using AutoMapper;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IMapper;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Mapper;
using Benaa.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace Benaa.Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region Services
            //services.AddScoped<ICustomerService, CustomerService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<UserManager<User>>();

            #endregion

            #region Repositories
            //services.AddTransient<ICustomerRepository, CustomerRepository>();
            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<IOrderRepository, OrderRepository>();
            //services.AddTransient<IOrderDetailsRepository, OrderDetailsRepository>();

            #endregion

            #region Mapper
            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Customer, CustomerViewModel>();
            //    cfg.CreateMap<CustomerViewModel, Customer>();

            //    cfg.CreateMap<Product, ProductViewModel>();
            //    cfg.CreateMap<ProductViewModel, Product>();

            //    cfg.CreateMap<Order, OrderViewModel>();
            //    cfg.CreateMap<OrderViewModel, Order>();
            //});

            //IMapper mapper = configuration.CreateMapper();

            //// Register the IMapperService implementation with your dependency injection container
            //services.AddSingleton<IBaseMapper<Customer, CustomerViewModel>>(new BaseMapper<Customer, CustomerViewModel>(mapper));
            //services.AddSingleton<IBaseMapper<CustomerViewModel, Customer>>(new BaseMapper<CustomerViewModel, Customer>(mapper));

            //services.AddSingleton<IBaseMapper<Product, ProductViewModel>>(new BaseMapper<Product, ProductViewModel>(mapper));
            //services.AddSingleton<IBaseMapper<ProductViewModel, Product>>(new BaseMapper<ProductViewModel, Product>(mapper));

            //services.AddSingleton<IBaseMapper<Order, OrderViewModel>>(new BaseMapper<Order, OrderViewModel>(mapper));
            //services.AddSingleton<IBaseMapper<OrderViewModel, Order>>(new BaseMapper<OrderViewModel, Order>(mapper));

            #endregion

            return services;
        }
    }
}

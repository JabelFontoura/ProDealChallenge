using Microsoft.Extensions.DependencyInjection;
using ProDeal.Application.Interfaces;
using ProDeal.Application.Mapper;
using ProDeal.Application.Services;

namespace ProDeal.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IFolderItemWriteService, FolderItemWriteService>();
            services.AddScoped<IFolderItemReadService, FolderItemReadService>();
            services.AddAutoMapper(typeof(AutoMapperConfiguration));

            return services;
        }
    }
}

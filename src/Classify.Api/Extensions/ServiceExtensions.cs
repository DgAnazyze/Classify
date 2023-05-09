using Classify.DataAccess.Interfaces;
using Classify.DataAccess.Repositories;
using Classify.Service.Interfaces;
using Classify.Domain.Entities;
using Classify.Service.Services;

namespace Classify.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddService(this IServiceCollection services)
        { 
            services.AddScoped<IExcelReaderService, ExcelReaderService>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}

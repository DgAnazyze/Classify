using Classify.DataAccess.Interfaces;
using Classify.DataAccess.Repositories;
using Classify.Service.Interfaces;
using Classify.Domain.Entities;
using Classify.Service.Services;

namespace Classify.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomerService(this IServiceCollection services)
        { 
            services.AddScoped<IRepository<Student>, GenericRepository<Student>>();

            services.AddScoped<IExcelReaderService, ExcelReaderService>();
        }
    }
}

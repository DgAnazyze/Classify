using Classify.Domain.Entities;

namespace Classify.Service.Interfaces;

public interface IExcelReaderService
{
    public Task<bool> GetFromExcelAsync(string path);
}

using ASP.Net.Application.Models;

namespace ASP.Net.Application.Abstractions
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorages();
        int AddStorage(StorageDto storage);
    }
}

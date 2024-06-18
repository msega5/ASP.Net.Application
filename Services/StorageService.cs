using ASP.Net.Application.Abstractions;
using ASP.Net.Application.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client.Extensions.Msal;

namespace ASP.Net.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public StorageService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        
        public int AddStorage(StorageDto storage)
        {
            using (_context)
            {
                var entity = _mapper.Map<StorageEntity>(storage);

                _context.Storages.Add(entity);
                _context.SaveChanges();
                _cache.Remove("storages");

                return entity.Id;
            }
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            using (_context)
            {
                if (_cache.TryGetValue("products", out List<StorageDto> storages))
                    return storages;

                storages = _context.Storages.Select(x => _mapper.Map<StorageDto>(x)).ToList();
                _cache.Set("storages", storages, TimeSpan.FromMinutes(30));

                return storages;

            }
        }
    }
}

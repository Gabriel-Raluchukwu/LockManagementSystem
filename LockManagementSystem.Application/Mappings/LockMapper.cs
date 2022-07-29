using AutoMapper;

namespace LockManagementSystem.Application.Mappings;

public static class LockMapper
{
    public static IMapper Mapper => LazyMapper.Value;

    private static readonly Lazy<IMapper> LazyMapper = new Lazy<IMapper>(
        () =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            return config.CreateMapper();
        });
}
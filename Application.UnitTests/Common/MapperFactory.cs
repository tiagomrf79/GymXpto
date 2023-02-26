using Application.Mappings;
using AutoMapper;

namespace Application.UnitTests.Common;

public static class MapperFactory
{
    public static IMapper Create()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        return configurationProvider.CreateMapper();
    }
}
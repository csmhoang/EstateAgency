using AutoMapper;

namespace Core.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TDestination>(this object source, IConfigurationProvider config)
        {
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }
    }
}

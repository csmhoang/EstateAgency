﻿using AutoMapper;

namespace Core;

public static class MappingExtensions
{
    public static TDestination MapTo<TDestination>(this object source, IConfigurationProvider config)
    {
        var mapper = config.CreateMapper();
        return mapper.Map<TDestination>(source);
    }
}

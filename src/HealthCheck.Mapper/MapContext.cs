using System;

namespace HealthCheck.Mapper
{
    public static class MapContext<T>
    {
        public static T Map(object source) => AutoMapper.Mapper.Map<T>(source);
    }
}

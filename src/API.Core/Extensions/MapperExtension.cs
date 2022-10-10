using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace API.Core.Extensions
{
    public static class MapperExtension
    {
        public static List<TDto> MapList<TDto>(this IMapperBase mapper, IList items)
        {
            return (from object item in items select mapper.Map<TDto>(item)).ToList();
        }
    }
}

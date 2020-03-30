using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoMappingProfile());
            });
        }
    }
}

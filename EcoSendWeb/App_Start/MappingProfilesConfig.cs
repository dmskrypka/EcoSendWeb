using AutoMapper;

namespace EcoSendWeb.App_Start
{
    public static class MappingProfilesConfig
    {
        private static IMapper mapper;

        public static IMapper Mapper
        {
            get
            {
                return MappingProfilesConfig.mapper;
            }
        }

        public static void RegisterMapping()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<Mapping.View.HomeMappingProfile>();
                cfg.AddProfile<Mapping.View.AccountMappingProfile>();
                cfg.AddProfile<Mapping.View.ParcelMappingProfile>();

                cfg.AddProfile<Mapping.Service.HomeMappingProfile>();
                cfg.AddProfile<Mapping.Service.AccountMappingProfile>();
                cfg.AddProfile<Mapping.Service.ParcelMappingProfile>();
            });

            MappingProfilesConfig.mapper = configuration.CreateMapper();

            configuration.AssertConfigurationIsValid();
        }
    }
}
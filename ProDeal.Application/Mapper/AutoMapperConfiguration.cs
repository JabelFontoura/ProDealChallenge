using AutoMapper;

namespace ProDeal.Application.Mapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(options =>
            {
                options.AddProfile(new ItemFolderProfile());
            });
        }
    }
}

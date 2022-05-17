using AutoMapper;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;

namespace HomeBookkeepingWebApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x => {
                x.CreateMap<Transaction, TransactionDTO>().ReverseMap();
                x.CreateMap<User, UserDTO>().ReverseMap();
                x.CreateMap<СreditСard, СreditСardDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}

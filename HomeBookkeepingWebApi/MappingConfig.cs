using AutoMapper;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;
using HomeBookkeepingWebApi.Domain.Entity.TemporaryData;

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

                x.CreateMap<TemporaryData_ReportTime, TemporaryData_ReportTimeDTO>().ReverseMap();
                x.CreateMap<TemporaryData_ReportCategoty, TemporaryData_ReportCategotyDTO>().ReverseMap();
                x.CreateMap<TemporaryData_RecipientData, TemporaryData_RecipientDataDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}

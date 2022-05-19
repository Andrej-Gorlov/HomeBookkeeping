using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;
using HomeBookkeepingWebApi.Domain.Response;
using HomeBookkeepingWebApi.Service.Interfaces;

namespace HomeBookkeepingWebApi.Service.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRep;
        public UserService(IUserRepository userRep) => _userRep = userRep;
        public async Task<IBaseResponse<UserDTO>> CreateServiceAsync(UserDTO entity)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            if(entity.СreditСards.Count != 0)
            {
                СreditСardDTO сreditСard = new();
                сreditСard.UserFullName = entity.FullName;
                сreditСard.BankName = "-";
                сreditСard.Number = "-";
                сreditСard.L_Account = "-";
                entity.СreditСards.Add(сreditСard); 
            }
            var model = await _userRep.CreateAsync(entity);
            baseResponse.DisplayMessage = "Пользователь создан";
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _userRep.DeleteAsync(id);
            if (IsSuccess)
            {
                baseResponse.DisplayMessage = "Пользователь удален.";
            }
            if (!IsSuccess)
            {
                baseResponse.DisplayMessage = $"Пользователь c указанным id: {id} не найдена.";
            }
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<UserDTO>>> GetServiceAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<UserDTO>>();
            IEnumerable<UserDTO> usersDTO = await _userRep.GetAsync();
            if (usersDTO is null)
            {
                baseResponse.DisplayMessage = "Список всех пользователей пуст.";
            }
            else
            {
                baseResponse.DisplayMessage = "Список всех пользователей.";
            }
            baseResponse.Result = usersDTO;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> GetByFullNameServiceAsync(string fullName)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO model = await _userRep.GetByFullNameAsync(fullName);
            if (model is null)
            {
                baseResponse.DisplayMessage = $"Пользователь под именим [{fullName}] не найден.";
            }
            else
            {
                baseResponse.DisplayMessage = $"Вывод пользователя под именим [{fullName}]";
            }
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO model = await _userRep.GetByIdAsync(id);
            if (model is null)
            {
                baseResponse.DisplayMessage = $"Пользователь под id [{id}] не найдена";
            }
            else
            {
                baseResponse.DisplayMessage = $"Вывод пользователя под id [{id}]";
            }
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> UpdateServiceAsync(UserDTO entity)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO model = await _userRep.UpdateAsync(entity);
            baseResponse.DisplayMessage = "Пользователь обновился.";
            baseResponse.Result = model;
            return baseResponse;
        }
    }
}

using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Paging;
using HomeBookkeepingWebApi.Domain.Response;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Net;

namespace HomeBookkeepingWebApi.Service.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRep;
        private readonly IUserRepository _userRep;
        private readonly IСreditСardRepository _creditCardRep;
        public TransactionService(ITransactionRepository transactionRep, IUserRepository userRep, IСreditСardRepository creditCardRep)
        {
            _transactionRep = transactionRep;
            _userRep = userRep;
            _creditCardRep = creditCardRep;
        } 
        public async Task<IBaseResponse<TransactionDTO>> AddServiceAsync(TransactionDTO entity)
        {
            var baseResponse = new BaseResponse<TransactionDTO>();

            var users = await _userRep.GetAsync();
            var user = users.FirstOrDefault(
                x => x.FullName.ToUpper().Replace(" ", "") == entity.UserFullName.ToUpper().Replace(" ", ""));

            var cards = await _creditCardRep.GetAsync();
            var card = cards.FirstOrDefault(
                x => x.UserFullName.ToUpper().Replace(" ", "") == entity.UserFullName.ToUpper().Replace(" ", "")
                && x.Number.ToUpper().Replace(" ", "") == entity.NumberCardUser.ToUpper().Replace(" ", ""));

            if (user is null || card is null)
            {
                throw new NullReferenceException("Не найден пользователь или номер карты указанный в транзакции.");
            }
            TransactionDTO model = await _transactionRep.AddAsync(entity);
            baseResponse.DisplayMessage = "Транзакции создана";
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _transactionRep.DeleteAsync(id);
            if (IsSuccess)
            {
                baseResponse.DisplayMessage = "Транзакция удалена.";
            }
            if (!IsSuccess)
            {
                baseResponse.DisplayMessage = $"Транзакция c указанным id: {id} не найдена.";
            }
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(DateTime data)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _transactionRep.DeleteAsync(data);
            if (IsSuccess)
            {
                baseResponse.DisplayMessage = "Транзакция удалена.";
            }
            if (!IsSuccess)
            {
                baseResponse.DisplayMessage = $"Транзакция c указанной датой: {data} не найдена.";
            }
            baseResponse.Result = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<TransactionDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<TransactionDTO>>();
            IEnumerable<TransactionDTO> transactionsDTO = await _transactionRep.GetAsync();
            if (transactionsDTO is null)
            {
                baseResponse.DisplayMessage = "Список всех транзакций пуст.";
            }
            else
            {
                baseResponse.DisplayMessage = "Список всех транзакций.";
            }
            baseResponse.Result = PagedList<TransactionDTO>.ToPagedList(transactionsDTO, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<TransactionDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<TransactionDTO>();
            TransactionDTO model = await _transactionRep.GetByIdAsync(id);
            if (model is null)
            {
                baseResponse.DisplayMessage = $"Транзакция под id [{id}] не найдена";
            }
            else
            {
                baseResponse.DisplayMessage = $"Вывод транзакций под id [{id}]";
            }
            baseResponse.Result = model;
            return baseResponse;
        }
        public async Task<IBaseResponse<List<TransactionDTO>>> AddFileExcelServiceAsync(IFormFile fileExcel, string userFullName, string numberCardUser)
        {
            var users = await _userRep.GetAsync();
            var user = users.FirstOrDefault(
                x => x.FullName.ToUpper().Replace(" ", "") == userFullName.ToUpper().Replace(" ", ""));
            
            var cards = await _creditCardRep.GetAsync();
            var card = cards.FirstOrDefault(
                x => x.UserFullName.ToUpper().Replace(" ", "") == userFullName.ToUpper().Replace(" ", "")
                && x.Number.ToUpper().Replace(" ", "") == numberCardUser.ToUpper().Replace(" ", ""));
            if (user is null || card is null)
            {
                throw new NullReferenceException("Не найден пользователь или номер карты указанный в транзакции.");
            }
            var list = new List<TransactionDTO>();
            using (var stream = new MemoryStream())
            {
                await fileExcel.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Лист1"];
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 12; row <= rowCount; row++)
                    {
                        string RowEnd = worksheet.Cells[row, 1].Text.ToString();
                        if (RowEnd.Length >= 30)
                        {
                            if (RowEnd.Substring(0, 30) == "Реквизиты для перевода на счёт")
                            {
                                break;
                            }
                        }
                        bool isRow = worksheet.Cells[row, 1].Value.ToString() == "Продолжение на следующей странице"
                            || worksheet.Cells[row, 1].Value.ToString().Trim() == "ДАТА ОПЕРАЦИИ (МСК)\nДата обработки¹ и код авторизации";

                        if (!isRow)
                        {
                            if (worksheet.Cells[row, 13].Text.ToString().Trim()[0] == '+')
                            {

                               await _creditCardRep.EnrollmentAsync(card.BankName, numberCardUser,
                                   decimal.Parse(worksheet.Cells[row, 13].Value.ToString().Trim().Substring(1)));
                            }
                            else
                            {
                                string data = worksheet.Cells[row, 1].Text.ToString().Trim();
                                string time = worksheet.Cells[row, 2].Value.ToString().Trim();

                                int year = int.Parse(data.Substring(data.Length - 4));
                                int month = int.Parse(data[3].ToString() + data[4].ToString());
                                int day = int.Parse(data.Substring(0, 2));
                                int hour = int.Parse(time.Substring(0, 2));
                                int minute = int.Parse(time.Substring(time.Length - 2));

                                list.Add(new TransactionDTO
                                {
                                    UserFullName = userFullName,
                                    NumberCardUser = numberCardUser,
                                    DateOperations = new DateTime(year, month, day, hour, minute, 01),
                                    Category = worksheet.Cells[row, 5].Text.ToString().Trim(),
                                    RecipientName = worksheet.Cells[row + 1, 5].Text.ToString().Trim(),
                                    Sum = decimal.Parse(worksheet.Cells[row, 13].Value.ToString().Trim())
                                });
                            }
                            row++;
                        }
                    }
                }
            }
            var baseResponse = new BaseResponse<List<TransactionDTO>>();
            var transactions = new List<TransactionDTO>();
            foreach (TransactionDTO transaction in list)
            {
                TransactionDTO transactionDTO = await _transactionRep.AddAsync(transaction);
                transactions.Add(transactionDTO);
            }
            baseResponse.Result = transactions;
            baseResponse.DisplayMessage = $"Список транзакций из файла [ {fileExcel.FileName} ] добавлен.";
            return baseResponse;
        }
    }
}

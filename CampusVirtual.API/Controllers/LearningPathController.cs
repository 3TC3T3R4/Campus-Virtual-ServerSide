using AutoMapper;
using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace CampusVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPathController : ControllerBase
    {
        private readonly ILearningPathUseCase _learningPathUseCase;
        private readonly IMapper _mapper;


        public LearningPathController(ILearningPathUseCase learningPathUseCase, IMapper mapper)
        {
            _learningPathUseCase = learningPathUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<LearningPath>> GetLearningPathsAsync()
        {
            return await _learningPathUseCase.GetLearningPathsAsync();
        }


        [HttpPost]
        [Route("CreateLearningPath")]
        public async Task<InsertNewLearningPath> CreateLearningPathAsync(InsertNewLearningPath newLearningPath)
        {
            return await _learningPathUseCase.CreateLearningPathAsync(_mapper.Map<LearningPath>(newLearningPath));
        }

        //[HttpGet]
        //[Route("GetCustomerWithAccountAndCard")]
        //public async Task<CustomerWithAccountAndCard> GetCustomerWithAccountAndCard(int id)
        //{
        //    return await _customerUseCase.GetCustomerWithAccountAndCard(id);
        //}


        //[HttpGet]
        //[Route("GetCustomerWithAccounts")]
        //public async Task<CustomerWithAccountsOnly> GetCustomerWithAccountsAsync(int id)
        //{
        //    return await _customerUseCase.GetCustomerWithAccountsAsync(id);
        //}

        //[HttpGet("GetCustomerWithAccount,Card,Transactions")]
        ////[Route("GetCustomerWithAccountsAndCardsAndTransactions")]
        //public async Task<CustomerWithAccounts> Get_DoneTransaction_By_AccountAndCardAsync(int id)
        //{
        //    return await _customerUseCase.GetDoneTransactionById(id);

        //}






    }
}

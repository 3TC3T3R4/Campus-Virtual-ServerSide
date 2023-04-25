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
        public async Task<LearningPath> CreateLearningPathAsync([FromBody]InsertNewLearningPath newLearningPath)
        {
            return await _learningPathUseCase.CreateLearningPathAsync(_mapper.Map<LearningPath>(newLearningPath));
        }

        [HttpGet("GetByCoach")]
        public async Task<List<LearningPath>> Get_LearningPaths_Coach(string id)
        {
            return await _learningPathUseCase.GetLearningPathsByCoachAsync(id);
        }

        [HttpPut]
        public async Task<InsertNewLearningPath> Update_Learning_By_Id(string id, [FromBody] InsertNewLearningPath command)
        {
            return await _learningPathUseCase.UpdateLearningPathByIdAsync(id, command);
        }

        [HttpDelete("{id:int}")]
        public async Task<string> Delete_LearningPath_By_Id(string id)
        {
            return await _learningPathUseCase.DeleteLearningPathByIdAsync(id);
        }

        [HttpGet("GetById")]
        public async Task<LearningPath> Get_LearningPath_Id(string id)
        {
            return await _learningPathUseCase.GetLearningPathByIdAsync(id);
        }


        [HttpPatch("UpdateDuration")]
        public async Task<string> UpdateLearningPathDuration(string id, int numberCourses)
        {
            return await _learningPathUseCase.UpdateLearningPathDurationAsync(id, numberCourses);
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

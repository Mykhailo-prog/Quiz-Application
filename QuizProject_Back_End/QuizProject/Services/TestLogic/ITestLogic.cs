using QuizProject.Models.DTO;
using QuizProject.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

//TODO: every class, interface, enum etc should has separated file
//TODO: Please read about SOLID. If you want to read/write/change/ statistic it should be separated service etc. S - Single Responsibility Principle
namespace QuizProject.Services.TestLogic
{
    public interface ITestLogic
    {
        public UserManagerResponse<FinishTestResponse> GetScore(QuizUser user, UserUpdateDTO userResult, Test test);
        public Task<UserManagerResponse> ChangeUserStatistic(QuizUser user, Test test, FinishTestResponse result, int id);
        /*
        */

    }
    //TODO: free space
    //DONE
}

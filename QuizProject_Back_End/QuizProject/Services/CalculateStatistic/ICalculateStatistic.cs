using QuizProject.Models;
using QuizProject.Models.Entity;
using QuizProject.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    public interface ICalculateStatistic
    {
        public UserManagerResponse ChangeAvrTries(TestStatistic stat, UserStatistic currUserStat, List<UserStatistic> userStats, FinishTestResponse result);
        public UserManagerResponse ChangeMinTries(TestStatistic stat, UserStatistic currUserStat, QuizUser user);
        public UserManagerResponse ChangeBestResult(TestStatistic stat, UserStatistic currUserStat, QuizUser user, FinishTestResponse result);
        public UserManagerResponse ChangeAllTriesCount(TestStatistic stat, List<UserStatistic> userStat);
        public UserManagerResponse ChangeBestTime(TestStatistic stat, UserStatistic currUserStat, List<UserStatistic> userStat, FinishTestResponse result);

    }
}

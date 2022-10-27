using QuizProject.Models;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    public interface ICalculateStatistic
    {
        Task<UserManagerResponse> CalculateStat(int id, QuizUser user, UserStatistic currUserStat, FinishTestResponse result);

    }
}

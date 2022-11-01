using QuizProject.Models;
using QuizProject.Models.Entity;
using QuizProject.Models.ResponseModels;
using System.Threading.Tasks;

namespace QuizProject.Services.RepositoryService.RepositoryAbstractions
{
    public abstract class StatisticAbstraction<T, K> : RepositoryAbstraction<T>, IStatisticRepositiry<T, K> where T : class
    {
        protected StatisticAbstraction(QuizContext context) : base(context)
        {
        }

        public abstract Task<UserManagerResponse> Create(K item);
        public abstract Task<UserManagerResponse> Update(UserStatistic currUserStat, TestLogicContainer<FinishTestResponse> container);
    }
}

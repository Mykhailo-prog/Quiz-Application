using QuizProject.Services.RepositoryService.Interfaces;

namespace QuizProject.Services.RepositoryService
{
    public interface IStatisticRepositiry<T, K> : IDefaultRepository<T, K> where T : class
    {

    }
}

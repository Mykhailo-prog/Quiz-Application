using QuizProject.Services.RepositoryService.Interfaces;

namespace QuizProject.Services.RepositoryService
{
    public interface ITestConnectionRepository<T, K> : IDefaultRepository<T, K> where T : class
    {

    }
}

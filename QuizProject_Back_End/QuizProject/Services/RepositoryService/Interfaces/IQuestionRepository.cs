using QuizProject.Services.RepositoryService.Interfaces;

namespace QuizProject.Services.RepositoryService
{
    public interface IQuestionRepository<T, K> : IDefaultRepository<T, K> where T : class
    {

    }
}

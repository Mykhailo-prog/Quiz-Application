using QuizProject.Models;

namespace QuizProject.Services.TestLogic
{
    public class TestLogicContainer<T>
    {
        public QuizUser User { get; set; }
        public Test Test { get; set; }
        public T Result { get; set; }
    }
}

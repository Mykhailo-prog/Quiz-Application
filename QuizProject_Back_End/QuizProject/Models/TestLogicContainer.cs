using QuizProject.Models.Entity;

namespace QuizProject.Models
{
    public class TestLogicContainer<T>
    {
        public QuizUser User { get; set; }
        public Test Test { get; set; }
        public T Result { get; set; }
    }
}

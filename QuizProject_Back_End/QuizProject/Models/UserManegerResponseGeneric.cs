namespace QuizProject.Models
{
    public class UserManagerResponse<T> : UserManagerResponse
    {
        public string Token { get; set; }
        public T Object { get; set; }
    }
}

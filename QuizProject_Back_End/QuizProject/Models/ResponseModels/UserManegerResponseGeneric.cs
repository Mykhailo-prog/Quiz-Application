namespace QuizProject.Models.ResponseModels
{
    public class UserManagerResponse<T> : UserManagerResponse
    {
        public string Token { get; set; }
        public T Object { get; set; }
    }
}

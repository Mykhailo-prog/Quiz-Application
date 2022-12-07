namespace QuizProject.Models.AppData
{
    public class AppConf
    {
        public string AppUrl { get; set; }
        public string FrontUrl { get; set; }
        public Jwt Jwt { get; set; }
        public EmailData Email { get; set; }
    }
}

using System;

namespace QuizProject.Services.CalculateStatistic
{
    class Time
    {
        public int Min { get; set; }
        public int Sec { get; set; }
        public Time(string stringTime)
        {
            Min = Convert.ToInt32(stringTime.Split(':')[0]);
            Sec = Convert.ToInt32(stringTime.Split(':')[1]);
        }

        public override string ToString()
        {
            return $"{Min}:{Sec}";
        }
    }
}

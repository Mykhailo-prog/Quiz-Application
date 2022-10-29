using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuizProject.Services.CalculateStatistic
{
    public class CalculateStatistic : ICalculateStatistic
    {
        private readonly QuizContext _db;
        public CalculateStatistic(QuizContext db)
        {
            _db = db;
        }

        public UserManagerResponse ChangeAvrTries(TestStatistic stat, UserStatistic currUserStat, List<UserStatistic> userStats, FinishTestResponse result)
        {
            try
            {
                if (currUserStat.Result == 100)
                {
                    result.Achievements.Add("100% Correct!");
                    if (currUserStat.TriesCount == 1)
                    {
                        stat.AvgTryCount++;
                        result.Achievements.Add("First 100% result!");
                    }
                }

                double param = stat.AvgTryCount * 100 / userStats.Count();
                stat.AvgFirstTryResult = Convert.ToInt32(Math.Round(param));

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Average tries changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Average tries changing failed",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public UserManagerResponse ChangeMinTries(TestStatistic stat, UserStatistic currUserStat, QuizUser user)
        {
            try
            {
                if (currUserStat.Result == 100)
                {
                    if (stat.MinTries == 0)
                    {
                        stat.MinTries = currUserStat.TriesCount;
                        stat.MinTriesUser = user.Login;
                    }
                    else
                    {
                        if (currUserStat.TriesCount < stat.MinTries)
                        {
                            stat.MinTries = currUserStat.TriesCount;
                            stat.MinTriesUser = user.Login;
                        }
                    }
                }

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Min tries changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Min tries changing failed",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public UserManagerResponse ChangeBestResult(TestStatistic stat, UserStatistic currUserStat, QuizUser user, FinishTestResponse result)
        {
            try
            {
                if (stat.BestResult != 100)
                {
                    if (stat.BestResult == 0)
                    {
                        stat.BestResult = currUserStat.Result;
                        stat.BestResultUser = user.Login;
                        result.Achievements.Add("You set the best result!");
                    }
                    else
                    {
                        if (currUserStat.Result > stat.BestResult)
                        {
                            stat.BestResult = currUserStat.Result;
                            stat.BestResultUser = user.Login;
                            result.Achievements.Add("You beat the best result!");
                        }
                    }

                }

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Best result changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Best result changing failed",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public UserManagerResponse ChangeAllTriesCount(TestStatistic stat, List<UserStatistic> userStat)
        {
            try
            {
                int count = 0;

                foreach (var user in userStat)
                {
                    count += user.TriesCount;
                }

                stat.CountOfAllTries = count;

                return new UserManagerResponse
                {
                    Success = true,
                    Message = "All tries count changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "All tries count changing failed",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public UserManagerResponse ChangeBestTime(TestStatistic stat, UserStatistic currUserStat, List<UserStatistic> userStat, FinishTestResponse result)
        {

            try
            {
                if (currUserStat.Result == 100)
                {
                    var ParsedTime = new List<Time>();

                    foreach (var u in userStat)
                    {
                        ParsedTime.Add(new Time(u.Time));
                    }

                    var Rtime = ParsedTime.OrderBy(x => x.Sec).OrderBy(x => x.Min);
                    string res = Rtime.FirstOrDefault().ToString();

                    stat.BestTime = res;
                    stat.BestTimeUser = _db.QuizUsers.Find(userStat.FirstOrDefault(u => u.Time == res).QuizUserId).Login;

                    if (stat.BestResultUser == result.UserName)
                    {
                        result.Achievements.Append("You beat a time record!");
                    }
                }
                return new UserManagerResponse
                {
                    Success = true,
                    Message = "Best time changed successfully",
                };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Success = false,
                    Message = "Best time changing failed",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurveyDemoApp.Models;
using System;
using System.Linq;

namespace SurveyDemoApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurveyDemoAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurveyDemoAppContext>>()))
            {
                // Look for any movies.
                if (context.Question.Any())
                {
                    return;   // DB has been seeded
                }

                context.Question.AddRange(
                    new Question
                    {
                        QuestionText = "Did you make this with MVC?",
                        AskingDate = DateTime.Parse("1989-2-12"),
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

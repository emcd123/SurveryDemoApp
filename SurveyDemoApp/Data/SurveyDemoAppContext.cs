using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyDemoApp.Models;

namespace SurveyDemoApp.Models
{
    public class SurveyDemoAppContext : DbContext
    {
        public SurveyDemoAppContext (DbContextOptions<SurveyDemoAppContext> options)
            : base(options)
        {
        }

        public DbSet<SurveyDemoApp.Models.User> User { get; set; }

        public DbSet<SurveyDemoApp.Models.Question> Question { get; set; }

        public DbSet<SurveyDemoApp.Models.Submission> Submission { get; set; }
    }
}

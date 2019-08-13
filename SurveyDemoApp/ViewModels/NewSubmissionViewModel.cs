using SurveyDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyDemoApp.ViewModels
{
    public class NewSubmissionViewModel
    {
        public IEnumerable<Question> allQuestions { get; set; }

        public IEnumerable<Submission> allSubmissions { get; set; } 
    }
}

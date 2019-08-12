using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyDemoApp.Models
{
    public class Submission
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int QuestionId { get; set; }

        public string AnswerText { get; set; }
    }
}

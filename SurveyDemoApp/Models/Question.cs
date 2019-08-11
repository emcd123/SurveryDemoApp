using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyDemoApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        [DataType(DataType.Date)]
        public DateTime AskingDate { get; set; }
    }
}

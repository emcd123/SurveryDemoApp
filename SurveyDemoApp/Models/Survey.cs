using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyDemoApp.Models
{
    public class Survey
    {
        public int Id { get; set; }

        public int Title { get; set; }

        [NotMapped]
        private List<string> _questionIds { get; set; }

        [NotMapped]
        public List<string> QuestionIds
        {
            get { return _questionIds; }
            set { _questionIds = value; }
        }

        [Required]
        public string QuestionIdsForDB
        {
            get { return String.Join(',', _questionIds); }
            set { _questionIds = value.Split(',').ToList(); }
        }
    }
}

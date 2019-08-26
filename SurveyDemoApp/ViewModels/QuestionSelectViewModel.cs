using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyDemoApp.ViewModels
{
    public class QuestionSelectViewModel
    {
        public List<QuestionSelection> QuestionSelections { set; get; }
    }

    public class QuestionSelection
    {
        public bool IsSelected { set; get; }
        public string Text { set; get; }
        public int Id { set; get; }
    }
}

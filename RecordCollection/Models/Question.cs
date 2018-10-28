using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordCollection.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public int Answer { get; set; }
        public Dictionary<int, string> QuestionChoices = new Dictionary<int, string>();
        public int SelectedAnswer { get; set; }
        public Question (int id, string questionName, int answer)
        {
            Id = id;
            QuestionName = questionName;
            Answer = answer;
        }

        public Question()
        {

        }

        public static Question GetQuestionById(int id)
        {
            var questions = Quiz.GetQuestions();
            return questions.Where(q => q.Id == id).First();
        }

        public void AddQuestionChoices(int optionId, string questionChoice)
        {
            QuestionChoices.Add(optionId, questionChoice);
        }
    }
}
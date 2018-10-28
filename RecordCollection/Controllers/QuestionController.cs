using RecordCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecordCollection.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Quiz
        public ActionResult Index()
        {
            var questions = Quiz.GetQuestions();
            return View(questions);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            Dictionary<Question, bool> isRightList = new Dictionary<Question, bool>();

            for(var i = 0; i < fc.Count; i++)
            {
                bool isRight = false;
                var question = Question.GetQuestionById(int.Parse(fc.Keys[i]));
                question.SelectedAnswer = int.Parse(fc[i]);
                bool checkedRadio = false;
                if (question.Answer == question.SelectedAnswer)
                {
                    isRight = true;
                    checkedRadio = true;
                    var choiceCount = question.QuestionChoices.Count;
                    for (var j = 0; j < choiceCount; j++)
                    {
                        if(j == int.Parse(fc.Keys[i]))
                            ViewData[string.Format("{0}-{1}_Checked", question.Id, j)] = string.Format("{0}", checkedRadio);
                    }
                }

                isRightList.Add(question, isRight);
                ViewData[question.Id.ToString()] = isRight.ToString();

            }
            var numberOfRightAnswers = isRightList.Where(a => a.Value == true).Count();
            var count = isRightList.Count();

            var percentage = Math.Round(((decimal)numberOfRightAnswers / count) * 100, 1);
            ViewBag.Percentage = percentage + "%";
            var questions = Quiz.GetQuestions();
            return View(questions);
        }

        [HttpPost]
        public ActionResult Submit(FormCollection fc)
        {
            Dictionary<Question, bool> isRightList = new Dictionary<Question, bool>();

            for (var i = 0; i < fc.Count; i++)
            {
                bool isRight = false;
                var question = Question.GetQuestionById(int.Parse(fc.Keys[i]));

                question.SelectedAnswer = int.Parse(fc[i]);
                var selectedAnswerText = question.QuestionChoices[question.SelectedAnswer];
                var rightAnswerText = question.QuestionChoices[question.Answer];
                if (question.Answer == question.SelectedAnswer)
                {
                    isRight = true;
                }

                isRightList.Add(question, isRight);
                if(isRight)
                    ViewData[question.Id.ToString()] = "Correct";
                else
                    ViewData[question.Id.ToString()] = "Incorrect";
                ViewData[string.Format("{0}-SelectedAnswer", question.Id)] = string.Format("Your answer: {0}",selectedAnswerText);
                ViewData[string.Format("{0}-RightAnswer", question.Id)] = string.Format("Right Answer: {0}", rightAnswerText);
            }
            var numberOfRightAnswers = isRightList.Where(a => a.Value == true).Count();
            var count = isRightList.Count();

            var percentage = Math.Round(((decimal)numberOfRightAnswers / count) * 100, 1);
            ViewBag.Percentage = "Score: "+ percentage + "%";
            var questions = Quiz.GetQuestions();
            return View(questions);
        }
    }
}
using RecordCollection.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace RecordCollection
{
    public class Quiz
    {
        public static List<Models.Question> GetQuestions()
        {
            var question = new Models.Question();
            var questions = new List<Models.Question>();
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList parentNode;
            int i = 0;
            FileStream fs = new FileStream(@"C:\Users\Daniel\Documents\Visual Studio 2015\Projects\RecordCollection\RecordCollection\quiz.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            parentNode = xmldoc.GetElementsByTagName("question");
            for (i = 0; i <= parentNode.Count - 1; i++)
            {
                Dictionary<int, string> choices = new Dictionary<int, string>();
                var id = int.Parse(parentNode[i].ChildNodes.Item(0).InnerText);
                var questionName = parentNode[i].ChildNodes.Item(1).InnerText;
                var answer = int.Parse(parentNode[i].ChildNodes.Item(3).Attributes[0].Value);
                question = new Question(id, questionName, answer);
                var choices1 = parentNode[i].ChildNodes.Item(2).ChildNodes;
                for (var j =0; j < choices1.Count; j++)
                {
                    var choiceText = choices1[j].InnerText;
                    var optionId = int.Parse(choices1[j].Attributes[0].Value);
                    question.AddQuestionChoices(optionId, choiceText);
                }
                questions.Add(question);
            }

            return questions;
        }
    }
}
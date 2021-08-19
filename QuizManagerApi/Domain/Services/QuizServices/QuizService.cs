using System;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;
using System.Linq;

namespace QuizManagerApi.Domain.Services
{
    public class QuizService
    {
        //public QuizService()
        //{

        //}

        private readonly QuizConnection _quizConnection;

        private readonly QuestionService _questionService;

        private readonly AnswerOptionService _answerOptionService;

        public QuizService(MySqlConnection conn)
        {
            _quizConnection = new QuizConnection(conn);
            _questionService = new QuestionService(conn);
            _answerOptionService = new AnswerOptionService(conn);
        }


        public IEnumerable<Quiz> GetAllActiveQuizzes()
        {
            IEnumerable<Quiz> _quizzes = _quizConnection.GetAllActiveQuizzes();

            return _quizzes.AsEnumerable();
        }

        public IEnumerable<QuizQuestion> GetQuizQuestionsByQuizId(int QuizId)
        {
            IEnumerable<QuizQuestion> _questions = _questionService.GetQuizQuestionsByQuizId(QuizId);

            return _questions;
        }

        public Quiz CreateNewQuiz(QuizHasQuestionsAndAnswers NewQuiz)
        {
            Quiz _newQuiz = new Quiz();


            _newQuiz.Name = NewQuiz.QuizName;
            _newQuiz.IsActive = true;

            _newQuiz = _quizConnection.CreateNewQuiz(_newQuiz);
            //Global.Quizzes.Add() 

            List<QuizQuestion> _quizQuestions = MapQuestionsToQuiz(_newQuiz.Id, NewQuiz);

            //foreach(QuizQuestion question in _quizQuestions)
            //{

            //}

            return _newQuiz;
        }

        public List<QuizQuestion> MapQuestionsToQuiz(int NewQuizId, QuizHasQuestionsAndAnswers NewQuiz)
        {
            List<QuizQuestion> _quizQuestions = new List<QuizQuestion>();
            List<AnswerOption> _questionAnswers = new List<AnswerOption>();

            foreach (QuestionHasAnswers question in NewQuiz.QuestionWithAnswers)
            {
                QuizQuestion _quizQuestion = new QuizQuestion();
                _quizQuestion.Question = question.Question;
                _quizQuestion.IsActive = true;
                _quizQuestion.QuizId = NewQuizId;

                QuizQuestion _createdQuestion = _questionService.CreateNewQuestion(_quizQuestion);
                _quizQuestions.Add(_createdQuestion);

                foreach (AnswerWithIsCorrect answer in question.Answers)
                {
                    _questionAnswers.Add(MapAnswersToQuestion(_createdQuestion.Id, answer));


                }

            }

            return _quizQuestions;
        }

        public AnswerOption MapAnswersToQuestion(int QuestionId, AnswerWithIsCorrect Answer)
        {
            AnswerOption _answer = new AnswerOption();

            _answer.QuestionId = QuestionId;
            _answer.Option = Answer.Answer;
            _answer.IsCorrectOption = Answer.IsCorrect;

            _answer = _answerOptionService.CreateNewAnswerOption(_answer);

            return _answer;
        }
    }
}

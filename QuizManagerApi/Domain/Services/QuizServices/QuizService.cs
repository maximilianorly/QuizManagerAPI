using System;
using QuizManagerApi.Domain.Connections;
using System.Collections.Generic;
using QuizManagerApi.Domain.Models;
using MySql.Data.MySqlClient;
using System.Linq;
using QuizManagerApi.Domain.Enums;

namespace QuizManagerApi.Domain.Services
{
    public class QuizService
    {
        private readonly QuizConnection _quizConnection;

        private readonly QuestionService _questionService;

        private readonly AnswerOptionService _answerOptionService;
        private readonly UserService _userService;

        public QuizService(MySqlConnection conn)
        {
            _quizConnection = new QuizConnection(conn);
            _questionService = new QuestionService(conn);
            _answerOptionService = new AnswerOptionService(conn);
            _userService = new UserService(conn);
        }

        public IEnumerable<Quiz> GetAllQuizzes()
        {
            List<Quiz> _quizzes = _quizConnection.GetAllQuizzes();

            return _quizzes.AsEnumerable();
        }

        public IEnumerable<Quiz> GetAllActiveQuizzes()
        {
            List<Quiz> _quizzes = _quizConnection.GetAllActiveQuizzes();

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

            foreach (QuestionHasAnswers question in NewQuiz.QuestionsWithAnswers)
            {
                QuizQuestion _quizQuestions = MapQuestionToQuiz(_newQuiz.Id, question);
            }

            return _newQuiz;
        }

        public QuizQuestion MapQuestionToQuiz(int NewQuizId, QuestionHasAnswers NewQuestionWithAnswers)
        {
            List<AnswerOption> _questionAnswers = new List<AnswerOption>();

                QuizQuestion _quizQuestion = new QuizQuestion();
                _quizQuestion.Question = NewQuestionWithAnswers.Question;
                _quizQuestion.IsActive = true;
                _quizQuestion.QuizId = NewQuizId;

                QuizQuestion _createdQuestion = _questionService.CreateNewQuestion(_quizQuestion);

                foreach (AnswerWithIsCorrect answer in NewQuestionWithAnswers.Answers)
                {
                    _questionAnswers.Add(MapAnswersToQuestion(_createdQuestion.Id, answer));


                }

            return _createdQuestion;
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

        public Quiz UpdateQuizSetIsActive(int QuizId, bool IsActive)
        {
            bool _newIsActiveValue = !IsActive;
            Quiz _quiz = _quizConnection.UpdateQuizSetIsActive(QuizId, _newIsActiveValue);

            return _quiz;
        }

        public IEnumerable<QuizQuestion> UpdateQuiz(int QuizId, int UserId, QuestionHasAnswers NewQuestionWithAnswers, int? QuestionId)
        {
            int accessLevel = _userService.GetUserAccessByUserId(UserId).AccessLevelId;

            if (!accessLevel.Equals((int)UserAccessEnum.Admin))
            {
                return null;
            }

            if (QuestionId != null)
            {
                QuizQuestion _quizQuestion = _questionService.GetQuestionById(QuestionId.Value);

                if (NewQuestionWithAnswers.Question != _quizQuestion.Question)
                {
                    QuizQuestion _setQuestionInactive = _questionService.SetQuestionActiveState(QuestionId.Value, false);
                    QuizQuestion _newQuestion = MapQuestionToQuiz(QuizId, NewQuestionWithAnswers);
                }
                else
                {
                    _answerOptionService.DeleteAnswersForQuestion(QuestionId.Value);

                    NewQuestionWithAnswers.Answers.ForEach(answer =>
                    {
                        MapAnswersToQuestion(QuestionId.Value, answer);
                    });
                }
            }
            else
            {
                QuizQuestion _newQuestion = MapQuestionToQuiz(QuizId, NewQuestionWithAnswers);
            }


            return GetQuizQuestionsByQuizId(QuizId);
        }
    }

}

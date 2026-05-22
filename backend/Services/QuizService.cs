using backend.DTOs;
using backend.Entities;
using backend.Exceptions;
using backend.Repositories;

namespace backend.Services
{
    public class QuizService
    {
        private readonly IQuizRepository _repository;

        public QuizService(IQuizRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<QuizQuestion>> GetAllQuestions()
        {
            return await _repository.GetAllQuestionsAsync();
        }

        public async Task<List<QuizTableItemDTO>> GetAllQuizzes()
        {
            return await _repository.GetAllQuizzesAsync();
        }

        public async Task<List<QuizTableItemDTO>> GetYourQuizzes(int userId)
        {
            return await _repository.GetYourQuizzesAsync(userId);
        }

        public async Task<QuizDetailDTO?> GetQuizResponses(int quizId)
        {
            return await _repository.GetQuizDetailAsync(quizId);
        }

        public async Task SaveNewQuiz(int userId, List<AnswerDTO> answers)
        {
            if (answers == null || !answers.Any())
            {
                throw new BadRequestException("Nu au fost trimise răspunsuri.");
            }

            var newQuiz = new Quiz
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            foreach (var answer in answers)
            {
                foreach (var optionId in answer.Options)
                {
                    var quizAnswer = new QuizAnswer
                    {
                        QuizQuestionId = answer.Question,
                        QuizOptionId = optionId,
                        Weight = answer.Weight
                    };

                    newQuiz.Answers.Add(quizAnswer);
                }
            }

            await _repository.SaveNewQuizAsync(newQuiz);
        }

        public async Task ToggleActiveStatus(int quizId)
        {
            await _repository.ToggleActiveStatusAsync(quizId);
        }
    }
}

using backend.DatabaseContext;
using backend.DTOs;
using backend.Entities;
using backend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface IQuizRepository
    {
        Task<List<QuizQuestion>> GetAllQuestionsAsync();
        Task SaveNewQuizAsync(Quiz quiz);
        Task<List<QuizTableItemDTO>> GetAllQuizzesAsync();
        Task<List<QuizTableItemDTO>> GetYourQuizzesAsync(int userId);
        Task<QuizDetailDTO?> GetQuizDetailAsync(int quizId);
        Task ToggleActiveStatusAsync(int quizId);
    }
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;
        public QuizRepository(AppDbContext appDbContext) 
        { 
            _context = appDbContext;
        }
        public async Task<List<QuizQuestion>> GetAllQuestionsAsync()
        {
            return await _context.QuizQuestions
                .Include(q => q.Options)
                .ToListAsync();
        }

        public async Task<List<QuizTableItemDTO>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes
                .Select(q => new QuizTableItemDTO
                {
                    Id = q.Id,
                    Name = q.User!.Name,
                    Email = q.User!.Email,
                    CreatedAt = q.CreatedAt,
                    IsActive = q.IsActive
                })
                .OrderByDescending(dto => dto.IsActive)
                .ThenByDescending(dto => dto.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<QuizTableItemDTO>> GetYourQuizzesAsync(int userId)
        {
            return await _context.Quizzes
                .Where(q => q.UserId == userId)
                .Select(q => new QuizTableItemDTO
                {
                    Id = q.Id,
                    Name = q.User!.Name,
                    Email = q.User!.Email,
                    CreatedAt = q.CreatedAt,
                    IsActive = q.IsActive
                })
                .OrderByDescending(dto => dto.IsActive)
                .ThenByDescending(dto => dto.CreatedAt)
                .ToListAsync();
        }

        public async Task<QuizDetailDTO?> GetQuizDetailAsync(int quizId)
        {
            return await _context.Quizzes
                .Where(q => q.Id == quizId)
                .Select(q => new QuizDetailDTO
                {
                    Id = q.Id,
                    Name = q.User!.Name,
                    Email = q.User!.Email,
                    CreatedAt = q.CreatedAt,
                    IsActive = q.IsActive,
                    Answers = q.Answers
                        .GroupBy(qa => qa.QuizQuestionId)
                        .Select(g => new QuizDetailAnswerDTO
                        {
                            Question = g.First().Question.Title,
                            Responses = g.Select(qa => qa.SelectedOption.Label).ToList(),
                            Weight = g.FirstOrDefault().Weight
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task SaveNewQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleActiveStatusAsync(int quizId)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);

            if (quiz == null)
            {
                throw new NotFoundException("Chestionarul nu a fost găsit.");
            }

            quiz.IsActive = !quiz.IsActive;
            await _context.SaveChangesAsync();
        }
    }
}

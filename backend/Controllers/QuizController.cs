using backend.DTOs;
using backend.Entities;
using backend.Exceptions;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("get-quiz-questions")]
        public async Task<ActionResult<List<QuizQuestion>>> GetAllQuestions()
        {
            var questions = await _quizService.GetAllQuestions();

            return Ok(questions);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-all-quizzes")]
        public async Task<ActionResult<List<QuizQuestion>>> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzes();

            return Ok(quizzes);
        }

        [Authorize]
        [HttpGet("get-your-quizzes")]
        public async Task<ActionResult<List<QuizQuestion>>> GetYourQuizzes()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(new { message = "Token invalid sau ID utilizator lipsă/incorect format." });
            }

            var quizzes = await _quizService.GetYourQuizzes(userId);

            return Ok(quizzes);
        }

        [Authorize]
        [HttpGet("get-quiz/{id}")]
        public async Task<ActionResult<QuizDetailDTO>> GetQuizResponses(int id)
        {
            var quiz = await _quizService.GetQuizResponses(id);

            return Ok(quiz);
        }

        [Authorize]
        [HttpPost("save-new-quiz")]
        public async Task<IActionResult> PostNewQuiz([FromBody] List<AnswerDTO> answers)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(new { message = "Token invalid sau ID utilizator lipsă/incorect format." });
            }

            try
            {
                await _quizService.SaveNewQuiz(userId, answers);
                return Ok(new { message = "Răspunsurile au fost salvate cu succes!" });
            }
            catch (BadRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "A aparut o eroare, te rugam sa reincerci!" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("toggle-quiz-status/{id}")]
        public async Task<IActionResult> ToggleQuizStatus(int id)
        {
            await _quizService.ToggleActiveStatus(id);

            return Ok(new { message = "Starea a fost actualizată cu succes." });
        }
    }
}

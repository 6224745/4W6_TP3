using flappyBirb_serveur.Data;
using flappyBirb_serveur.Models;
using flappyBirb_serveur.Models.DTO;
using flappyBirb_serveur.Services;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace flappyBirb_serveur.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ScoresController : ControllerBase
    {
        private readonly FlappyBirdService _flappybirdService;
        private readonly UserManager<User> _userManager;

        public ScoresController(FlappyBirdService flappybirdService, UserManager<User> userManager)
        {
            _flappybirdService = flappybirdService;
            _userManager = userManager;
        }

        // GET: api/Scores
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Score>>?> GetPublicsScores()
        { 
            return await _flappybirdService.GetPublicsScores();
        }

        // GET: api/Scores/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>?> GetMyScores()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Utilisateur non authentifié" });
            }

            return await _flappybirdService.GetMyScores(userId);
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeScoreVisibility(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Utilisateur non authentifié" });
            }

            User? user = await _flappybirdService.GetUserById(userId);

            if (user == null)
            {
                return NotFound(new { Message = "Utilisateur non trouvé" });
            }

            Score? score = await _flappybirdService.GetScoreById(id);
            if (score == null || score.UserId != userId)
            {
                return Unauthorized();
            }


            if (score != null)
            {
                await _flappybirdService.ChangeScoreVisibility(score);
            }
            return Ok(score);
        }

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score?>> PostScore(ScoreDTO score)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Utilisateur non authentifié" });
            }

            User? user = await _flappybirdService.GetUserById(userId);

            if (user == null)
            {
                return NotFound(new { Message = "Utilisateur non trouvé" });
            }

            Score newScore = new Score()
            {
                Pseudo = user.UserName,
                ScoreValue = score.ScoreValue,
                TimeInSeconds = score.TimeInSeconds,
                Date = DateTime.Now.ToString(),
                IsPublic = score.IsPublic,
                UserId = userId
            };

            return await _flappybirdService.CreateScore(newScore);
        }
    }
}

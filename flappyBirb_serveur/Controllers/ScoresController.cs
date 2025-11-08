using flappyBirb_serveur.Data;
using flappyBirb_serveur.Models;
using flappyBirb_serveur.Models.DTO;
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
        private readonly flappyBirb_serveurContext _context;
        private readonly UserManager<User> _userManager;

        public ScoresController(flappyBirb_serveurContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Scores
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Score>>> GetPublicsScores()
        { 
            return await _context.Score.Where(u => u.IsPublic).OrderByDescending(u => u.ScoreValue).Take(10).ToListAsync();
        }

        // GET: api/Scores/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetMyScores()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Utilisateur non authentifié" });
            }
            var scoreList = await _context.Score.Where(s => s.UserId == userId).OrderByDescending(s => s.ScoreValue).ToListAsync();

            return scoreList;
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeScoreVisibility(int id, Score score)
        {
            if (id != score.Id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(ScoreDTO score)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Utilisateur non authentifié" });
            }

            User? user = await _context.Users.FindAsync(userId);

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

            _context.Score.Add(newScore);
            await _context.SaveChangesAsync();

            return Ok(newScore);
        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.Id == id);
        }
    }
}

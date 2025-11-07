using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flappyBirb_serveur.Data;
using flappyBirb_serveur.Models;

namespace flappyBirb_serveur.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly flappyBirb_serveurContext _context;

        public ScoresController(flappyBirb_serveurContext context)
        {
            _context = context;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetPublicsScores()
        {
            List<Score>? publicScores = await _context.Score.Where(u => u.IsPublic).OrderByDescending(u => u.ScoreValue).ToListAsync();
            if (publicScores == null || publicScores != null)
            {
                return Ok(publicScores);
            }
            return publicScores;
        }

        // GET: api/Scores/5
        [HttpGet]
        public async Task<ActionResult<Score>> GetMyScores(int id)
        {
            var score = await _context.Score.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
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
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            Score newScore = new Score()
            {
                Id = score.Id,
                Pseudo = score.Pseudo,
                ScoreValue = score.ScoreValue,
                TimeInSeconds = score.TimeInSeconds,
                Date = DateTime.Now.ToString(),
                IsPublic = score.IsPublic
            };
            _context.Score.Add(newScore);
            await _context.SaveChangesAsync();

            return newScore;
        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.Id == id);
        }
    }
}

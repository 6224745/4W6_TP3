using flappyBirb_serveur.Data;
using flappyBirb_serveur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace flappyBirb_serveur.Services
{
    public class FlappyBirdService
    {
        private readonly flappyBirb_serveurContext _context;

        public FlappyBirdService(flappyBirb_serveurContext context)
        {
            _context = context;
        }

        private bool IsContextValid() => _context != null && _context.Score != null;

        public async Task<ActionResult<IEnumerable<Score>>?> GetPublicsScores()
        {
            if (!IsContextValid()) return null;
            return await _context.Score.Where(u => u.IsPublic).OrderByDescending(u => u.ScoreValue).Take(10).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Score>>?> GetMyScores(string userId)
        {
            if (!IsContextValid()) return null;
            var scoreList = await _context.Score.Where(s => s.UserId == userId).OrderByDescending(s => s.ScoreValue).ToListAsync();

            return scoreList;
        }

        public async Task<User?> GetUserById(string userId)
        {
            if (!IsContextValid()) return null;
            return await _context.Users.FindAsync(userId);
        }

        public async Task<Score?> GetScoreById(int id)
        {
            if (!IsContextValid()) return null;
            return await _context.Score.FindAsync(id);
        }

        public async Task<Score?> CreateScore(Score score)
        {
            if (!IsContextValid()) return null;

            _context.Score.Add(score);
            await _context.SaveChangesAsync();
            return score;
        }

        public async Task<Score?> ChangeScoreVisibility(Score score)
        {
            if (!IsContextValid()) return null;

            score.IsPublic = !score.IsPublic;
            await _context.SaveChangesAsync();
            return score;
        }
    }
}

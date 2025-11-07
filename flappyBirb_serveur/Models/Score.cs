namespace flappyBirb_serveur.Models
{
    public class Score
    {
        public int Id { get; set; }
        public string? Pseudo { get; set; } = null;
        public string? Date { get; set; } = null;
        public float TimeInSeconds { get; set; }
        public int ScoreValue { get; set; }
        public bool IsPublic { get; set; }
    }
}

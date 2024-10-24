using System;

namespace CodingTask.Data.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public int GeneratedNumber { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
        public Match Match { get; set; }
    }
}

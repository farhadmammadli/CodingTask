using System.Collections.Generic;

namespace CodingTask.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<Submission> Submissions { get; set; }
        public ICollection<Match> MatchesWon { get; set; }
    }
}

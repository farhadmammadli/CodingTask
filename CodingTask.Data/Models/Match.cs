using System;
using System.Collections.Generic;

namespace CodingTask.Data.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime ExpiryTimestamp { get; set; }
        public int? WinnerUserId { get; set; }

        public User WinnerUser { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}

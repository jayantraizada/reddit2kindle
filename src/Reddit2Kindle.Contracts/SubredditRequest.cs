namespace Reddit2Kindle.Contracts
{
    public class SubredditRequest : Request
    {
        public string Subreddit { get; init; }

        public TimePeriod TimePeriod { get; init; } = TimePeriod.Week;
    }
}

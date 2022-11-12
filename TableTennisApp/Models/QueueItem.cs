namespace TableTennisApp.Models
{
    public class QueueItem
    {
        public Guid Id { get; set; }
        public int OrdinalNumber { get; set; }
        public Guid PlayerId { get; set; }
        public Player Player { get; set; } = null!;
    }
}

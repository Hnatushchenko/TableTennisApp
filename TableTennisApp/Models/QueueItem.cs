namespace TableTennisApp.Models
{
    public class QueueItem
    {
        public Guid Id { get; set; }
        public int OrdinalNumber { get; set; }
        public Guid PlayerId { get; set; }
        public ApplicationUser Player { get; set; } = null!;
    }
}

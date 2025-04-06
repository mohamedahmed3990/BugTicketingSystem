namespace BugTicketingSystem.BLL.DTOs
{
    public record BugChildDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
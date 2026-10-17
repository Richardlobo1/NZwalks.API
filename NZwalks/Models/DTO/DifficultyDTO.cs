namespace NZwalks.API.Models.DTO
{
    public class DifficultyDTO
    {
        public Guid Id { get; set; }
        required
        public string Name
        { get; set; }
    }
}

namespace TaskApi.Models
{
  public class Task
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Status { get; set; }
    public DateTime RegisterDate { get; set; }
  }

}
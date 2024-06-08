namespace LibraryWebApi.Common.Models
{
  public class Reservation
  {
    public int Id { get; set; }
    public Guid BookId { get; set; }
    public bool IsActive { get; set; }
  }
}

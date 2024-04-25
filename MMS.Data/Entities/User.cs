namespace MMS.Data.Entities;

public enum Role { admin, guest, contributor }


public class User {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}

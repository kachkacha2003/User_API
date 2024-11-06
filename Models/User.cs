namespace MyWalletApi.Models
{
    public class User
    {
        Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public User(string username,string email,string passwordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Id = Guid.NewGuid();
            
        }
    }
}

﻿namespace MyWalletApi.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

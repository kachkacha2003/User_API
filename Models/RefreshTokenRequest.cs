namespace MyWalletApi.Models
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public RefreshTokenRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
            
        }
    }
}

namespace SSE.Identidade.API.Model
{
    public class UserResponseLogin
    {
        public string AccesToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}

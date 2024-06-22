using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSE.Identidade.API.Extensions;
using SSE.Identidade.API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SSE.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AuthController> _logger;

        public AuthController(SignInManager<IdentityUser> signInManager, 
                              UserManager<IdentityUser> userManager, 
                              IOptions<AppSettings> appSettings,
                              ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterViewModel registerUser)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User registered and token generated");
                return Ok(await GenerateJwt(registerUser.Email));
           
            }

            return BadRequest();

        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginViewModel loginUser)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, isPersistent: false, true);

            if(result.Succeeded)
            {
                
                return Ok(await GenerateJwt(loginUser.Email));
            }

            return BadRequest();

        }
    
        private async Task<UserResponseLogin> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Sender,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            var response = new UserResponseLogin
            {
                AccesToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken 
                {
                    Id = encodedToken,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        
    }
}

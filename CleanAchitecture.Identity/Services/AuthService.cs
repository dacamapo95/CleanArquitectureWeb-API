using CleanAchitecture.Identity.Models;
using CleanArquitecture.Application.Contracts.Identity;
using CleanArquitecture.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace CleanAchitecture.Identity.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager,
                          SignInManager<ApplicationUser> signInManager,
                          IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe.");
            }

            var AuthResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!AuthResult.Succeeded)
            {
                throw new Exception($"Failed to login {request.Email}");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            return new AuthResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName
            };
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            if (await _userManager.FindByNameAsync(request.UserName) != null)
                throw new Exception($"The username already exists {request.UserName}");

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                throw new Exception($"The username with email {request.Email} already exists.");

            var newUser = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.Name,
                UserName = request.UserName,
                EmailConfirmed = true,
            };

            var result = _userManager.CreateAsync(newUser);

            if (!result.IsCompletedSuccessfully)
                throw new Exception($"Error creando el usuario.");

            await _userManager.AddToRoleAsync(newUser, "Operator");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(newUser);

            return new RegistrationResponse()
            {
                Email = newUser.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserId = newUser.Id,
                Username = newUser.UserName,
            };
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser applicationUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(applicationUser);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            var roleClaims = new List<Claim>();

            IEnumerable<Claim> claims =
                roles
                .Select(role => new Claim(ClaimTypes.Role, role))
                .Concat(new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName)
                        })
                .Concat(userClaims)
                .Distinct();

            SymmetricSecurityKey symmetricSecurityKey =
                new(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Key));

            SigningCredentials signingCredentials =
                new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken =
                new(issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtSettings.Duration),
                    signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}

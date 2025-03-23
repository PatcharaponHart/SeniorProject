using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using YourNamespace.Models;
using YourNamespace.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly CurriculumDbContext _context;

    private readonly IConfiguration _config;

    public AuthController(CurriculumDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        var student = await _context.tblStudents
            .Where(s => s.Username == login.Username && s.Password == login.Password)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var tokenString = GenerateJWTToken(student);
        return Ok(new { Token = tokenString, StudentID = student.StudentID, Name = student.FirstName + " " + student.LastName });
    }

    private string GenerateJWTToken(Students student)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, student.Username),
            new Claim("StudentID", student.StudentID)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

// Model สำหรับรับค่า Login
public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

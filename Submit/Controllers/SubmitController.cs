using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Submit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmitController : ControllerBase
    {
        private readonly SubmissionDbContext _context;
        private readonly HttpClient _httpClient;

        public SubmitController(SubmissionDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCode([FromBody] string code)
        {
            // Get the token from the Authorization header
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("No token provided.");
            }

            // Fetch user data from Auth API
            var userId = await GetUserFromAuthApiAsync(token);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Invalid token.");
            }

            // Simulate score calculation
            var score = new Random().Next(0, 101);

            var submission = new CodeSubmission
            {
                UserId = userId,  // Use the user ID from Auth API
                Code = code,
                Score = score,
                SubmissionDate = DateTime.UtcNow
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Code submitted successfully", Score = score });
        }

        private async Task<string?> GetUserFromAuthApiAsync(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("https://your-auth-api.com/api/users/me");

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();  // Define User model
                    return user?.Id;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Submit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmitController : ControllerBase
    {
        private readonly SubmissionDbContext _context;

        public SubmitController(SubmissionDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitCode([FromBody] SubmitDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var submission = new CodeSubmission
            {
                UserId = userId,
                Code = dto.Code,
                Score = await CalculateScoreAsync(),
                SubmissionDate = DateTime.UtcNow
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return Ok(submission);
        }

        private async Task<int> CalculateScoreAsync()
        {
            var random = new Random();
            int delay = random.Next(60_000, 120_000);
            await Task.Delay(delay);
            return random.Next(101);
        }
    }
}
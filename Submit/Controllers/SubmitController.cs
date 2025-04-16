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

            const int MillisecondsInOneMinute = 60_000;
            const int MillisecondsInTwoMinutes = 120_000;

            int delayInMilliseconds = random.Next(MillisecondsInOneMinute, MillisecondsInTwoMinutes);

            await Task.Delay(delayInMilliseconds);

            return random.Next(101);
        }
    }
}

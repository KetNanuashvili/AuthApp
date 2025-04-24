using Microsoft.EntityFrameworkCore;

public class SubmissionDbContext : DbContext
{
    public SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : base(options) { }

    public DbSet<CodeSubmission> Submissions { get; set; } = null!;
}
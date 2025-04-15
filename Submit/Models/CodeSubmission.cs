using System;

public class CodeSubmission
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int Score { get; set; }
    public DateTime SubmissionDate { get; set; }
}

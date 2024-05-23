using System; // Add this line

public class MockEmailService : EmailService
{
    public MockEmailService(IConfiguration configuration) : base(configuration)
    {
    }

    public override async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        toEmail = "malakhelmy2004@gmail.com";
        Console.WriteLine($"Mock email sent to: {toEmail}, Subject: {subject}, Body: {body}");
        await Task.CompletedTask;
    }
}

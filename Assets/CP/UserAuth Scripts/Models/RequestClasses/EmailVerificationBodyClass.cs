using System;

public class EmailVerificationBodyClass
{
    public string email { get; set; }
    public string otp { get; set; }

    public EmailVerificationBodyClass(string userEmail, string userOTP)
    {
        this.email = userEmail;
        this.otp = userOTP;
    }
}


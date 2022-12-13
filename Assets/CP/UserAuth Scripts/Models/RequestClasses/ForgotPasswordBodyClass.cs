using System;

public class ForgotPasswordBodyClass
{
    public string email { get; set; }
    public ForgotPasswordBodyClass(string userEmail)
    {
        this.email = userEmail;
    }
}


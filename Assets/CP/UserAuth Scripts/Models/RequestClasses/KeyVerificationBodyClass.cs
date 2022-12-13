using System;

public class KeyVerificationBodyClass
{
    public string email { get; set; }
    public string password_reset_key { get; set; }

    public KeyVerificationBodyClass(string userEmail, string userKey)
    {
        this.email = userEmail;
        this.password_reset_key = userKey;
    }
}


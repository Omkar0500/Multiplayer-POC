using System;

public class PasswordVerificationBodyClass
{
    public string email { get; set; }
    public string password_reset_key { get; set; }
    public string password { get; set; }

    public PasswordVerificationBodyClass(string userEmail, string otpKey, string updatedPassword)
    {
        this.email = userEmail;
        this.password_reset_key = otpKey;
        this.password = updatedPassword;
    }
}
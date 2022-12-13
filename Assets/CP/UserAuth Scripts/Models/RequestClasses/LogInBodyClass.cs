using System;

public class LogInBodyClass
{
    public string email { get; set; }
    public string password { get; set; }

    public LogInBodyClass(string userEmail, string userPassword)
    {
        this.email = userEmail;
        this.password = userPassword;
    }
}


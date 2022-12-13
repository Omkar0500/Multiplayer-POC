using System;

public class ChangePasswordBodyClass
{
    public string password { get; set; }
    public string newpassword { get; set; }

    public ChangePasswordBodyClass(string oldPass, string newPass)
    {
        this.password = oldPass;
        this.newpassword = newPass;
    }
}


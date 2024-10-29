[System.Serializable]
public class LoginPayload
{
    public string email;
    public string password;

    public LoginPayload(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}
using System.Collections.Generic;

[System.Serializable]
public class LoginResponse
{
    public int status;
    public string message;
    public string timestamp;
    public LoginData data;
}

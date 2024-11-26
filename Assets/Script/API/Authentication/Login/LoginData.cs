using System.Collections.Generic;

[System.Serializable]
public class LoginData
{
    public int id;
    public string username;
    public string accessToken;
    public string refreshToken;
    public string email;
    public List<GroupData> groups;
}

[System.Serializable]
public class RegisterPayload
{
    // UserAccount
    public string email;
    public string username;
    public string password;
    public string name;
    public string regNumber;
    public string scope;

    // Profile for Student and Teacher
    public string firstName;
    public string lastName;
    public string phoneNumber;
    public string address;
    public string city;
    public string region;
    public string country;
    public string zipcode;
    public string gender;

    // Profile for Student
    public string nim;

    // Profile for Teacher
    public string nid;
    public string urlGoogleScholar;

    
}

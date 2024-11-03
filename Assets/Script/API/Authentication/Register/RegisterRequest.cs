using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class RegisterRequest : MonoBehaviour
{
    private string registerUrl = AppConfig.BASE_URL + "/auth/signup";
    private UserService userService = new UserService();

    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField studentIDInputField;
    [SerializeField] private TMP_InputField registerYearInputField;
    [SerializeField] private TMP_Dropdown genderDropdown;
    [SerializeField] private TMP_Dropdown registrationTypeDropdown;

    public void OnRegisterButtonClicked()
    {
        // Gather user input
        string email = emailInputField.text.Trim();
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text.Trim();
        string name = nameInputField.text.Trim();
        string studentID = studentIDInputField.text.Trim();
        string registerYear = registerYearInputField.text.Trim();
        string gender = genderDropdown.options[genderDropdown.value].text;
        string registrationType = registrationTypeDropdown.options[registrationTypeDropdown.value].text;

        // Create JSON payload
        //string jsonPayload = JsonUtility.ToJson(new
        //{
        //    email = email,
        //    username = username,
        //    password = password,
        //    name = name,
        //    studentID = studentID,
        //    registerYear = registerYear,
        //    gender = gender
        //});

        // Create an instance of the RegisterData class
        RegisterPayload registerPayload = new RegisterPayload
        {
            email = email,
            username = username,
            password = password,
            name = name,
            studentId = studentID,
            registerYear = registerYear,
            gender = gender
        };

        string jsonPayload = JsonUtility.ToJson(registerPayload);

        Debug.Log($"JSON Payload: \n {jsonPayload}");
        string newEndpoint = "";

        switch (registrationType)
        {
            case "Student":
                newEndpoint = registerUrl + "/student";
                break;
            case "Teacher":
                newEndpoint = registerUrl + "/teacher"; 
                break;
            default:
                break;
        }

        Debug.Log($"Register URL: {newEndpoint}");
        StartCoroutine(SendRegisterRequest(newEndpoint, jsonPayload));
        //userService.RegisterUser(jsonPayload);
    }

    private IEnumerator SendRegisterRequest(string url, string jsonPayload)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // Set up the request
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Registration successful: " + request.downloadHandler.text);
                // Handle successful registration (e.g., navigate to another scene, show a message, etc.)
            }
            else
            {
                Debug.LogError("Registration error: " + request.error);
                // Handle registration failure (e.g., show error message to the user)
            }
        }
    }
}

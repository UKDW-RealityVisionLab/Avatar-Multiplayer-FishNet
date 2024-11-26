using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class RegisterRequest : MonoBehaviour
{
    private string registerUrl = AppConfig.BASE_URL + "/auth/signup";

    // common form
    [SerializeField] private TMP_Dropdown registrationTypeDropdown;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Dropdown genderDropdown;

    // for student registration form
    [SerializeField] private TMP_InputField studentIDInputField;

    // for teacher registration form
    [SerializeField] private TMP_InputField teacherIDInputField;

    void Start()
    {
        setTeacherFormVisibility(false);
    }
    public void OnRegisterButtonClicked()
    {
        // Gather user input
        string email = emailInputField.text.Trim();
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text.Trim();
        string name = nameInputField.text.Trim();
        string nim = studentIDInputField.text.Trim();
        string nid = teacherIDInputField.text.Trim();
        string gender = genderDropdown.options[genderDropdown.value].text;
        string registrationType = registrationTypeDropdown.options[registrationTypeDropdown.value].text;

        // Create an instance of the RegisterData class
        RegisterPayload registerPayload = new RegisterPayload
        {
            email = email,
            username = username,
            password = password,
            name = name,
            gender = gender,
            firstName = name

        };

        switch (registrationType)
        {
            case "Student":
                registerPayload.scope = "student";

                registerPayload.nim = nim;
                break;
            case "Teacher":
                registerPayload.scope = "teacher";

                registerPayload.nid = nid;
                break;
            default:
                break;
        }

        Debug.Log($"Register URL: {registerUrl}");

        string jsonPayload = JsonUtility.ToJson(registerPayload);
        Debug.Log($"JSON Payload: \n {jsonPayload}");
        StartCoroutine(SendRegisterRequest(registerUrl, jsonPayload));
    }

    public void OnRegistrationTypeValueChanged()
    {
        string registrationType = registrationTypeDropdown.options[registrationTypeDropdown.value].text;
        Debug.Log($"VALUE REGISTER TYPE CHANGED: {registrationType}");

        if( registrationType == "Student")
        {
            setTeacherFormVisibility(false);
            setStudentFormVisibility(true);
        }else if( registrationType == "Teacher")
        {
            setStudentFormVisibility(false);
            setTeacherFormVisibility(true);
        }
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

    private void setStudentFormVisibility(bool visibility)
    {
        studentIDInputField.gameObject.SetActive(visibility);
        resetAllFields();
    }

    private void setTeacherFormVisibility(bool visibility)
    {
        teacherIDInputField.gameObject.SetActive(visibility);
        teacherIDInputField.GetComponent<TMP_InputField>().text = string.Empty;
        resetAllFields(); 
    }

    private void resetAllFields()
    {
        emailInputField.GetComponent<TMP_InputField>().text = string.Empty;
        usernameInputField.GetComponent<TMP_InputField>().text = string.Empty;
        passwordInputField.GetComponent<TMP_InputField>().text = string.Empty;
        nameInputField.GetComponent<TMP_InputField>().text = string.Empty;
        genderDropdown.value = 0;

        //studentIDInputField.GetComponent<TMP_InputField>().text = string.Empty;
        //registerYearInputField.GetComponent<TMP_InputField>().text = string.Empty;

        //teacherIDInputField.GetComponent<TMP_InputField>().text = string.Empty;
        //employmentNumberInputField.GetComponent<TMP_InputField>().text = string.Empty;
    }
}

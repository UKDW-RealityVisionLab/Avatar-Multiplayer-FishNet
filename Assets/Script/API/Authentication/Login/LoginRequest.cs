using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginRequest : MonoBehaviour
{
    private string loginUrl = AppConfig.BASE_URL + "/auth/signin";

    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void StartLogin(string email, string password)
    {
        StartCoroutine(LoginCoroutine(email, password));
    }

    public void OnLoginButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        //string email = "prtha@student.ukdw.ac.id";
        //string password = "qwe123";
        StartLogin(email.Trim(), password.Trim());
    }

    private IEnumerator LoginCoroutine(string email, string password)
    {
        // Create the login payload
        LoginPayload payload = new LoginPayload(email, password);
        string jsonPayload = JsonConvert.SerializeObject(payload);

        // Create a UnityWebRequest with the POST method
        UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Deserialize JSON response
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(request.downloadHandler.text);
            LoginData data = loginResponse.data;

            Debug.Log("Login Successful: " + loginResponse.message);
            Debug.Log("Username: "+ data.username);

            // Use loginResponse data as needed, e.g., storing the access token
            TokenManager.Instance.LoginToken = data.accessToken;
            TokenManager.Instance.RefreshToken = data.refreshToken;

            Debug.Log(TokenManager.Instance.LoginToken);
            Debug.Log(TokenManager.Instance.RefreshToken);

            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
        else
        {
            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(request.downloadHandler.text);
            ErrorData data = errorResponse.data;

            Debug.LogError("Login Failed: " + data.message);
        }
    }
}

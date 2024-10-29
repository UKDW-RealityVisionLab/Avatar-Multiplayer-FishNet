using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PasswordInputField : MonoBehaviour
{
    public TMP_InputField TMPusername;
    public TMP_InputField TMPpassword;

    public Button logginButton;

    // Start is called before the first frame update
    void Start()
    {
        TMPpassword.contentType = TMP_InputField.ContentType.Password;
        logginButton.onClick.AddListener(onLogin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    async void onLogin()
    {
        Debug.Log("Username : " + TMPusername.text);
        Debug.Log("Password : " + TMPpassword.text);
        // Create the login request body
        LoginRequest loginRequest = new LoginRequest
        {
            email = TMPusername.text,
            password = TMPpassword.text
        };

        // Convert the request object to a JSON string
        string jsonBody = JsonUtility.ToJson(loginRequest);

        // Send the request to the API
        ResponseWrapper response = await SendPostRequest("localhost:8080/auth/signin", jsonBody);
        Debug.Log(response.data.accessToken);
    }

    // Method to send a POST request with a JSON body
    private async Task<ResponseWrapper> SendPostRequest(string url, string jsonBody)
    {
        // Create a new UnityWebRequest for a POST method
        UnityWebRequest www = new UnityWebRequest(url, "POST");

        // Convert the JSON string to a byte array for the UploadHandler
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // Set the content type to application/json
        www.SetRequestHeader("Content-Type", "application/json");

        // DownloadHandler to get the response
        www.downloadHandler = new DownloadHandlerBuffer();

        // Await the request completion
        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // Wait for the request to complete
        }

        // Handle possible errors
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
            return null;
        }

        // Parse the response
        ResponseWrapper response = JsonUtility.FromJson<ResponseWrapper>(www.downloadHandler.text);

        return response;
    }

    [Serializable]
    public class LoginRequest
    {
        public string email;
        public string password;
    }

    [Serializable]
    public class ResponseWrapper
    {
        public LoginResponse data;
        public int status;
        public string message;
    }


    [Serializable]
    public class LoginResponse
    {
        public long id;
        public string username;
        public string password;
        public string accessToken;
        public string idToken;
        public string refreshToken;
        public string fcmToken;
        public string regNumber;
        public string email;
        public string imageUrl;
        private HashSet<GroupEntity> groups;
    }


    [Serializable]
    public class GroupEntity
    {
        public long id;
        public string groupname;
        public long permission;
    }

}

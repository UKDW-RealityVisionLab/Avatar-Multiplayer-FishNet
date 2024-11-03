using UnityEngine;

public class UserService
{
    public void RegisterUser(string jsonPayload)
    {
        // Create a request builder for the registration endpoint
        ApiClient.Instance.CreateRequestBuilder("POST", "auth/signup")
            .SetJsonPayload(jsonPayload) // Set the JSON payload
            .SetHeader("Authorization", "Bearer your_token_here") // Set dynamic headers as needed
            .SetHeader("Custom-Header", "CustomValue") // Add other headers as needed
            .SendRequest(
                onSuccess: response => Debug.Log("Registration successful: " + response),
                onError: error => Debug.LogError("Registration error: " + error)
            );
    }
}
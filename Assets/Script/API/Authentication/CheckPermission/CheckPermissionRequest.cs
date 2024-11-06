using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static PermissionConstants;

public class CheckPermissionRequest : MonoBehaviour
{
    private string checkAccessUrl = AppConfig.BASE_URL + "/auth/apps-check-permission";

    // Serialize the enum for dropdown selection in the Inspector
    [SerializeField]
    private PermissionList _permissionAccessOption;

    public void CheckUserAccess(System.Action<bool> callback)
    {
        int resourceId = PermissionConstants.Permissions[_permissionAccessOption];
        StartCoroutine(CheckPermissionCoroutine(resourceId, callback));
    }

    private IEnumerator CheckPermissionCoroutine(long permissionId, System.Action<bool> callback)
    {
        // Create the login payload
        CheckPermissionPayload payload = new CheckPermissionPayload
        {
            featureCode = permissionId,
        };
        string jsonPayload = JsonConvert.SerializeObject(payload);
        Debug.Log("Payload : " + jsonPayload);

        // Create a UnityWebRequest with the POST method
        UnityWebRequest request = new UnityWebRequest(checkAccessUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        if (TokenManager.Instance.LoginToken != null)
        {
            Debug.Log($"LOGIN TOKEN IN CHECK PERMISSION: {TokenManager.Instance.LoginToken}");
            request.SetRequestHeader("Authorization", $"Bearer {TokenManager.Instance.LoginToken}");
        }

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        bool hasAccess = false;

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Deserialize JSON response
            CheckPermissionResponse response = JsonConvert.DeserializeObject<CheckPermissionResponse>(request.downloadHandler.text);
            CheckPermissionData data = response.data;
            hasAccess = data.status;

            Debug.Log($"message: {response.message}");
            Debug.Log("User Has Access? : " + response.data.status);
        }
        else
        {
            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(request.downloadHandler.text);
            ErrorData data = errorResponse.data;

            Debug.LogError("Check Access Failed: " + data);
        }
        callback?.Invoke(hasAccess);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClient : MonoBehaviour
{
    private static readonly string baseURL = AppConfig.BASE_URL;

    // Singleton instance for global access
    public static ApiClient Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist this instance across scenes
        }
        else
        {
            Destroy(gameObject);  // Enforce singleton
        }
    }

    // Public method for creating a request builder
    public RequestBuilder CreateRequestBuilder(string method, string endpoint)
    {
        return new RequestBuilder(method, endpoint);
    }

    public class RequestBuilder
    {
        private readonly string method;
        private readonly string endpoint;
        private readonly Dictionary<string, string> headers = new Dictionary<string, string>();
        private string jsonPayload;

        public RequestBuilder(string method, string endpoint)
        {
            this.method = method;
            this.endpoint = endpoint;
        }

        // Method to set headers
        public RequestBuilder SetHeader(string key, string value)
        {
            headers[key] = value;
            return this;
        }

        // Method to set the JSON payload
        public RequestBuilder SetJsonPayload(string json)
        {
            jsonPayload = json;
            return this;
        }

        // Execute the request
        public IEnumerator SendRequest(Action<string> onSuccess, Action<string> onError)
        {
            string url = $"{baseURL}/{endpoint}";
            using (UnityWebRequest request = new UnityWebRequest(url, method))
            {
                if (jsonPayload != null)
                {
                    byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonPayload);
                    request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                    request.SetRequestHeader("Content-Type", "application/json");
                }

                // Set additional headers
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }

                request.downloadHandler = new DownloadHandlerBuffer();

                // Send the request and wait for the response
                yield return request.SendWebRequest();

                // Check for network errors
                if (request.result == UnityWebRequest.Result.Success)
                {
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
                else
                {
                    onError?.Invoke(request.error);
                }
            }
        }
    }
}

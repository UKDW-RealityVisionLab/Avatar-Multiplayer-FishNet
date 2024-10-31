[System.Serializable]
public class ErrorResponse
{
    public ErrorData data;
    public int status;
    public string timestamp;
    public string message;
}

[System.Serializable]
public class ErrorData
{
    public string status;
    public string message;
}
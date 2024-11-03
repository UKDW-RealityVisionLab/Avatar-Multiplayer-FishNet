using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckPermissionResponse
{
    public int status;
    public CheckPermissionData data;
    public string message;
    public string timestamp;

}

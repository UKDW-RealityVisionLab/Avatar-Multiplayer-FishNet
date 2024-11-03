using UnityEngine;
public class TokenManager : MonoBehaviour
{
    public static TokenManager Instance { get; private set; }
    public string LoginToken { get; set; }
    public string RefreshToken { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This will keep the object alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
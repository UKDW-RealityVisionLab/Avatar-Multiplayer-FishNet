using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public GameObject selectedCharacter; // Karakter yang dipilih dari hierarchy
    public static CharacterManager instance;

    private void Awake()
    {
        // Singleton pattern untuk memastikan hanya ada satu CharacterManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan GameManager saat berpindah scene
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("CharacterManager Awake() called");
    }

    public void SelectCharacter(GameObject character)
    {
        selectedCharacter = character;
        DontDestroyOnLoad(selectedCharacter); // Jangan hancurkan karakter yang dipilih
    }

    public GameObject GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public void StartGame()
    {
        if (selectedCharacter != null)
        {
            SceneManager.LoadScene("Game"); // Ganti "GameScene" dengan nama scene game Anda
        }
        else
        {
            Debug.LogError("No character selected! Please select a character before starting the game.");
        }
    }
}

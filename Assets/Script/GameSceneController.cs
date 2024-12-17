using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public Transform spawnPoint; // Tempat karakter akan muncul di scene game

    private void Start()
    {
        // Ambil karakter yang dipilih dari CharacterManager
        GameObject character = CharacterManager.instance.GetSelectedCharacter();

        if (character != null)
        {
            // Pindahkan karakter ke posisi spawn point
            character.transform.position = spawnPoint.position;
            character.transform.rotation = spawnPoint.rotation;

            Debug.Log("Character spawned in game scene: " + character.name);
        }
        else
        {
            Debug.LogError("No active character found in game scene!");
        }
    }
}

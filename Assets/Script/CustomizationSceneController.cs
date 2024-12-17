using UnityEngine;

public class CustomizationSceneController : MonoBehaviour
{
    public CharacterManager characterManager;
    public GameObject boyCharacter;
    public GameObject girlCharacter;

    public void SelectBoy()
    {
        characterManager.SelectCharacter(boyCharacter);
        Debug.Log("Boy character selected.");
    }

    public void SelectGirl()
    {
        characterManager.SelectCharacter(girlCharacter);
        Debug.Log("Girl character selected.");
    }

    public void StartGame()
    {
        characterManager.StartGame();
    }
}

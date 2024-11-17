using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Tambahkan ini untuk mengakses UI
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public string sceneName; // Nama scene yang ingin dipindahkan
    private SceneManagerScript sceneManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Mendapatkan komponen SceneManagerScript dari GameObject yang sama
        sceneManagerScript = FindObjectOfType<SceneManagerScript>();

        // Jika Anda ingin menghubungkan tombol ini dengan event click
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // Fungsi yang dipanggil saat tombol diklik
    void OnButtonClick()
    {
        if (sceneManagerScript != null)
        {
            sceneManagerScript.LoadScene(sceneName);
        }
    }
}
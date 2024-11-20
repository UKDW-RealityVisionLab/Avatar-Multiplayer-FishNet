using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider audioVolumeSlider; // Slider untuk volume audio
    [SerializeField] Slider musicVolumeSlider; // Slider untuk volume musik

    private AudioSource musicSource; // AudioSource untuk musik

    void Start()
    {
        // Mendapatkan komponen AudioSource dari objek yang memiliki musik
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();

        // Mengatur slider ke nilai volume saat ini
        audioVolumeSlider.value = AudioListener.volume;
        musicVolumeSlider.value = musicSource.volume;

        // Menambahkan listener untuk slider
        audioVolumeSlider.onValueChanged.AddListener(SetAudioVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void Pause()
    {
        // Menampilkan menu pause
        pauseMenu.SetActive(true);
        // Menghentikan waktu
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
    }

    public void Resume()
    {
        // Menyembunyikan menu pause
        pauseMenu.SetActive(false);
        // Mengembalikan waktu ke normal
        Time.timeScale = 1f;
    }

    // Fungsi untuk mengatur volume audio
    public void SetAudioVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // Fungsi untuk mengatur volume musik
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
}
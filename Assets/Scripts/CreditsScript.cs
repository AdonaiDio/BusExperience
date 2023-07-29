using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    private SoundManager soundManager;
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        soundManager.StopBusMotorSFX();
        soundManager.PlayMusic(soundManager.musics[3]);
        soundManager.musicSource.volume = 0.7f;
    }
    public void GoToMenu()
    {
        soundManager.StopMusic();
        Destroy(soundManager.gameObject);
        SceneManager.LoadScene(0);
    }
}

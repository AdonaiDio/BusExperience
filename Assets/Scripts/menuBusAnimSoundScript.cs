using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuBusAnimSoundScript : MonoBehaviour
{
    private SoundManager soundManager;
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    public void IdleSound() {
        soundManager.StopBusMotorSFX();
        soundManager.PlayBusMotorSFX(soundManager.busMotorSfxs[3]);
    }
    public void StartMovingSound()
    {
        soundManager.StopBusMotorSFX();
        soundManager.PlayBusMotorSFX(soundManager.busMotorSfxs[1]);
    }
    public void MovingSound()
    {
        soundManager.StopBusMotorSFX();
        soundManager.PlayBusMotorSFX(soundManager.busMotorSfxs[0]);
    }
    public void GoToLevel1()
    {
        SceneManager.LoadScene(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private SoundManager soundManager;
    [SerializeField] private Animator busCamAnimator;
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        busCamAnimator.Play("MenuBusCameraIdle");
        soundManager.StopMusic();
        soundManager.PlayMusic(soundManager.musics[2]);
    }
        
    public void StartGameAnimation()
    {
        busCamAnimator.Play("MenuBusCameraStart");
        gameObject.SetActive(false);
    }
}

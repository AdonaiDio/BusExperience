using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PassengerList = System.Collections.Generic.List<Passenger>;

public class LevelManager : MonoBehaviour
{
    //controlar o fim de fase
    //Condição: KM maximo atingido ou Nenhum passageiro disponível no level(onibus e paradas)
    private SoundManager SM;

    private BusStopScript[] busStopsList;
    private PlayerBusScript playerBus;
    [SerializeField] private GameObject scorePanel;
    private InputManager inputManager;
    private SoundManager soundManager;

    public int lvlCosts; //meta da fase para poder ir para a próxima
    public int nextSceneIndex;

    private void Awake()
    {
        SM = FindObjectOfType<SoundManager>();
        busStopsList = FindObjectsOfType<BusStopScript>();
        playerBus = FindObjectOfType<PlayerBusScript>();
        inputManager = FindObjectOfType<InputManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    void Start()
    {
        //tocar musica
        SM.StopMusic();
        SM.PlayMusic(SM.musics[0]);
        SM.StopBusMotorSFX();
        SM.PlayBusMotorSFX(SM.busMotorSfxs[0]);
        SM.busMotorSource.volume = 0;
    }
    private void OnEnable()
    {
        Events.atDestinationEvent.AddListener(CheckForAllPassengersAtScene);
        Events.atDestinationEvent.AddListener(CheckKMTraveled);
        Events.RemovePassangerFromBusEvent.AddListener(CFAPAS_removePas);
        Events.OnEmbarkButtonEvent.AddListener(CFAPAS_onEmbarkBut);
    }
    private void OnDisable()
    {
        Events.atDestinationEvent.RemoveListener(CheckForAllPassengersAtScene);
        Events.atDestinationEvent.RemoveListener(CheckKMTraveled);
        Events.RemovePassangerFromBusEvent.RemoveListener(CFAPAS_removePas);
    }
    private void CFAPAS_removePas(Passenger p = null) { CheckForAllPassengersAtScene(); }
    private void CFAPAS_onEmbarkBut(PassengerList p = null, PassengerList p2 = null) { CheckForAllPassengersAtScene(); }
    private void CheckForAllPassengersAtScene(Transform t = null)
    {
        int hasPassengers = 0;
        //todas as paradas
        foreach (BusStopScript bss in busStopsList)
        {
            if (bss.GetPassengers().Count > 0)
                hasPassengers+=1;
        }
        //no onibus
        if (playerBus.passengers.Count > 0)
            hasPassengers += 1;
        //se tiver nehum passageiro na cena, então é hora de terminar a fase
        if (hasPassengers == 0)
        {
            StartScoreCount();
        }
    }
    private void CheckKMTraveled(Transform t = null)
    {
        if(playerBus.currentKMTraveled >= playerBus.maxKMCapacity)
        {
            StartScoreCount();
        }
    }

    private void StartScoreCount()
    {
        scorePanel.SetActive(true);
        inputManager.SwitchActionMapUIGaming("UI");
        soundManager.StopMusic();
        soundManager.StopBusMotorSFX();
        soundManager.StopSFX();
        soundManager.PlayMusic(soundManager.musics[1]);
        scorePanel.GetComponent<ScoreLevelScript>().StartCounting(playerBus.money, lvlCosts, nextSceneIndex);
    }
}

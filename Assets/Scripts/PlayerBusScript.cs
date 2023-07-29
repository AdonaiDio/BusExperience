using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBusScript : MonoBehaviour
{
    public Transform destinationTransform;
    private NavMeshAgent navMeshAgent;

    public Transform currentRoad; //rua que o bus está


    public int maxKMCapacity = 100;
    public int currentKMTraveled = 0;
    public List<Passenger> passengers;
    public int maxPassengerCapacity = 5;
    public int money; //talvez tenha que persistir entre cenas ou ir pra um script de playerData

    private SoundManager soundManager;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        //currentKMTraveled = 0;
    }
    void Update()
    {
        navMeshAgent.destination = destinationTransform.position;
        if (currentKMTraveled > maxKMCapacity) { currentKMTraveled = maxKMCapacity; }
    }
    private void OnEnable()
    {
        Events.RemovePassangerFromBusEvent.AddListener(RemovePassanger);
        Events.AddPassangerFromBusEvent.AddListener(AddPassanger);
        Events.OnEmbarkButtonEvent.AddListener(AddPassangersList);
        Events.StartToMoveBusEvent.AddListener(StartMovement);
    }


    private void OnDisable()
    {
        Events.RemovePassangerFromBusEvent.RemoveListener(RemovePassanger);
        Events.AddPassangerFromBusEvent.RemoveListener(AddPassanger);
        Events.OnEmbarkButtonEvent.RemoveListener(AddPassangersList);
        Events.StartToMoveBusEvent.RemoveListener(StartMovement);
    }

    private void StartMovement(int i = 0)
    {
        //play sound (baseado na velocidade
        //soundManager.PlayBusMotorSFX();
        //aumenta volume
    }

    private void AddPassangersList(List<Passenger> pEmbarkList, List<Passenger> pWaitList)
    {
        foreach (Passenger p in pEmbarkList)
        {
            AddPassanger(p);
        }
    }

    private void AddPassanger(Passenger p)
    {
        passengers.Add(p);
    }

    private void RemovePassanger(Passenger p)
    {
        passengers.Remove(p);
        money += p.Cash;
        //som de dinheiro
        soundManager.PlaySFX(soundManager.sfxs[0]);
        soundManager.PlaySFX(soundManager.sfxs[1]);
        Events.CashInEvent.Invoke();
    }

    private void AtDestination()
    {
        //if((transform.position - destinationTransform.position).magnitude <= 0.65f)
        //{
            //chegou no centro da rua
            Events.atDestinationEvent.Invoke(currentRoad);
        //}
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "road")
        {
            currentRoad = col.transform;
        }
        else if (col.transform == destinationTransform)
        {
            AtDestination();
        }
    }
}
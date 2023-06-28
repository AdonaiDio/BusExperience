using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBusScript : MonoBehaviour
{
    public Transform destinationTransform;
    private NavMeshAgent navMeshAgent;

    public Transform currentRoad; //rua que o bus está


    public int maxKMCapacity = 100;
    public List<Passenger> passengers;
    public int maxPassengerCapacity = 5;
    public int money; //talvez tenha que persistir entre cenas ou ir pra um script de playerData

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navMeshAgent.destination = destinationTransform.position;
    }



    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "road")
        {
            currentRoad = col.transform;
        }
    }
}
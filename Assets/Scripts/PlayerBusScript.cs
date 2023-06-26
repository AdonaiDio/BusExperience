using UnityEngine;
using UnityEngine.AI;

public class PlayerBusScript : MonoBehaviour
{
    public Transform destinationTransform;
    private NavMeshAgent navMeshAgent;

    public Transform currentRoad; //rua que o bus está

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